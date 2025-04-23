using Microsoft.EntityFrameworkCore;
using PuzzleLab.BackgroundWorker.Workers;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Infrastructure.Persistence;
using PuzzleLab.Infrastructure.Persistence.Repositories;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var dbConn = hostContext.Configuration.GetConnectionString("DefaultConnection")
                     ?? throw new InvalidOperationException("DefaultConnection missing");
        services.AddDbContext<DatabaseContext>(opts =>
            opts.UseNpgsql(dbConn, o =>
                    o.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IQuizSessionRepository, QuizSessionRepository>();
        services.AddScoped<IQuizAnswerRepository, QuizAnswerRepository>();

        var rmqUri = hostContext.Configuration.GetConnectionString("RabbitMq")
                     ?? throw new InvalidOperationException("RabbitMq missing");
        services.AddSingleton(rmqUri);

        services.AddHostedService<QuizFinalizationWorker>();
    })
    .Build();

await host.RunAsync();