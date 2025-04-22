using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Infrastructure.Persistence;
using PuzzleLab.Infrastructure.Persistence.Repositories;
using PuzzleLab.BackgroundWorker.Workers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                "Database connection string 'DefaultConnection' not found in configuration.");
        }

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(connectionString,
                npgsqlOptions => { npgsqlOptions.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName); });
            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserFactory>();

        services.AddScoped<IQuizSessionRepository, QuizSessionRepository>();
        services.AddScoped<IQuizAnswerRepository, QuizAnswerRepository>();

        services.AddHostedService<QuizFinalizationWorker>();
    })
    .Build();

await host.RunAsync();