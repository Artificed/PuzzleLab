using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Infrastructure.Persistence;
using PuzzleLab.Infrastructure.Persistence.Repositories;
using PuzzleLab.Infrastructure.Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(connectionString,
        npgsqlOptions => { npgsqlOptions.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName); });
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
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
    var scope = app.Services.CreateScope();
    var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    var userSeeder = new UserSeeder(databaseContext);
    await userSeeder.SeedUsersAsync();

    var questionPackageSeeder = new QuestionPackageSeeder(databaseContext);
    await questionPackageSeeder.SeedQuestionPackagesAsync();

    var questionSeeder = new QuestionSeeder(databaseContext);
    await questionSeeder.SeedQuestionsAsync();

    var answerSeeder = new AnswerSeeder(databaseContext);
    await answerSeeder.SeedAnswersAsync();

    var scheduleSeeder = new ScheduleSeeder(databaseContext);
    await scheduleSeeder.SeedSchedulesAsync();

    var quizSeeder = new QuizSeeder(databaseContext);
    await quizSeeder.SeedQuizzesAsync();

    var quizUserSeeder = new QuizUserSeeder(databaseContext);
    await quizUserSeeder.SeedQuizUsersAsync();

    var quizSessionSeeder = new QuizSessionSeeder(databaseContext);
    await quizSessionSeeder.SeedQuizSessionsAsync();

    var quizAnswerSeeder = new QuizAnswerSeeder(databaseContext);
    await quizAnswerSeeder.SeedQuizAnswersAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();