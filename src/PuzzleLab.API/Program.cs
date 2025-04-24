using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PuzzleLab.API.Hubs;
using PuzzleLab.API.Services;
using PuzzleLab.Application.Common.Interfaces;
using PuzzleLab.Domain.Common;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Infrastructure.Messaging;
using PuzzleLab.Infrastructure.Persistence;
using PuzzleLab.Infrastructure.Persistence.Repositories;
using PuzzleLab.Infrastructure.Persistence.Seeders;
using PuzzleLab.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionPackageRepository, QuestionPackageRepository>();
builder.Services.AddScoped<IQuizAnswerRepository, QuizAnswerRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizSessionRepository, QuizSessionRepository>();
builder.Services.AddScoped<IQuizUserRepository, QuizUserRepository>();
builder.Services.AddScoped<IQuizSessionQuestionRepository, QuizSessionQuestionRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<AnswerFactory>();
builder.Services.AddTransient<QuestionFactory>();
builder.Services.AddTransient<QuestionPackageFactory>();
builder.Services.AddTransient<QuizAnswerFactory>();
builder.Services.AddTransient<QuizFactory>();
builder.Services.AddTransient<QuizSessionFactory>();
builder.Services.AddTransient<QuizUserFactory>();
builder.Services.AddTransient<QuizSessionQuestionFactory>();
builder.Services.AddTransient<ScheduleFactory>();
builder.Services.AddTransient<UserFactory>();

builder.Services.AddScoped<AnswerSeeder>();
builder.Services.AddScoped<QuestionSeeder>();
builder.Services.AddScoped<QuestionPackageSeeder>();
builder.Services.AddScoped<QuizSeeder>();
builder.Services.AddScoped<QuizUserSeeder>();
builder.Services.AddScoped<ScheduleSeeder>();
builder.Services.AddScoped<UserSeeder>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(connectionString,
        npgsqlOptions => { npgsqlOptions.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName); });
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof
    (PuzzleLab.Application.Features.Auth.Commands.LoginCommand).Assembly));

var rabbitMqConnectionString = builder.Configuration.GetConnectionString("RabbitMq");

if (string.IsNullOrEmpty(rabbitMqConnectionString))
{
    throw new InvalidOperationException(
        "RabbitMQ connection string 'RabbitMq:ConnectionString' not found in configuration.");
}

IDomainEventDispatcher rabbitMqDispatcher = await RabbitMqDomainEventDispatcher.CreateAsync(rabbitMqConnectionString);
builder.Services.AddSingleton<IDomainEventDispatcher>(_ => rabbitMqDispatcher);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var issuer = jwtSettings["Issuer"]
             ?? throw new InvalidOperationException("JWT Issuer is not configured in JwtSettings:Issuer");
var audience = jwtSettings["Audience"]
               ?? throw new InvalidOperationException("JWT Audience is not configured in JwtSettings:Audience");
var secretKey = jwtSettings["Key"]
                ?? throw new InvalidOperationException("JWT Key is not configured in JwtSettings:Key");

var keyBytes = Encoding.UTF8.GetBytes(secretKey);
const int minKeySizeInBytes = 256 / 8; // HS256 requires 256 bits minimum key size
if (keyBytes.Length < minKeySizeInBytes)
{
    throw new InvalidOperationException(
        $"JWT Key must be at least {minKeySizeInBytes * 8} bits ({minKeySizeInBytes} bytes) long for HS256. Check JwtSettings:Key. Current length: {keyBytes.Length * 8} bits.");
}

var securityKey = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = builder.Environment.IsProduction();
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = securityKey,

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddSignalR();
builder.Services.AddHostedService<TimePublisherService>();

builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

var app = builder.Build();

if (args.Contains("--migrate"))
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}

if (args.Contains("--seed"))
{
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;

        var userSeeder = serviceProvider.GetRequiredService<UserSeeder>();
        await userSeeder.SeedUsersAsync();

        var questionPackageSeeder = serviceProvider.GetRequiredService<QuestionPackageSeeder>();
        await questionPackageSeeder.SeedQuestionPackagesAsync();

        var questionSeeder = serviceProvider.GetRequiredService<QuestionSeeder>();
        await questionSeeder.SeedQuestionsAsync();

        var answerSeeder = serviceProvider.GetRequiredService<AnswerSeeder>();
        await answerSeeder.SeedAnswersAsync();

        var scheduleSeeder = serviceProvider.GetRequiredService<ScheduleSeeder>();
        await scheduleSeeder.SeedSchedulesAsync();

        var quizSeeder = serviceProvider.GetRequiredService<QuizSeeder>();
        await quizSeeder.SeedQuizzesAsync();

        var quizUserSeeder = serviceProvider.GetRequiredService<QuizUserSeeder>();
        await quizUserSeeder.SeedQuizUsersAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<TimeHub>("/hubs/time");

app.Run();