using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Infrastructure.Persistence;
using PuzzleLab.Infrastructure.Persistence.Repositories;
using PuzzleLab.Infrastructure.Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionPackageRepository, QuestionPackageRepository>();
builder.Services.AddScoped<IQuizAnswerRepository, QuizAnswerRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizSessionRepository, QuizSessionRepository>();
builder.Services.AddScoped<IQuizUserRepository, QuizUserRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<AnswerFactory>();
builder.Services.AddTransient<QuestionFactory>();
builder.Services.AddTransient<QuestionPackageFactory>();
builder.Services.AddTransient<QuizAnswerFactory>();
builder.Services.AddTransient<QuizFactory>();
builder.Services.AddTransient<QuizSessionFactory>();
builder.Services.AddTransient<QuizUserFactory>();
builder.Services.AddTransient<ScheduleFactory>();
builder.Services.AddTransient<UserFactory>();

builder.Services.AddScoped<AnswerSeeder>();
builder.Services.AddScoped<QuestionSeeder>();
builder.Services.AddScoped<QuestionPackageSeeder>();
builder.Services.AddScoped<QuizAnswerSeeder>();
builder.Services.AddScoped<QuizSeeder>();
builder.Services.AddScoped<QuizSessionSeeder>();
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

builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssembly(typeof
        (PuzzleLab.Application.Features.Auth.Commands.LoginCommand).Assembly));

builder.Services.AddControllers();

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

        var quizSessionSeeder = serviceProvider.GetRequiredService<QuizSessionSeeder>();
        await quizSessionSeeder.SeedQuizSessionsAsync();

        var quizAnswerSeeder = serviceProvider.GetRequiredService<QuizAnswerSeeder>();
        await quizAnswerSeeder.SeedQuizAnswersAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();