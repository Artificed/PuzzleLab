using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.BackgroundWorker.Workers;

public class QuizFinalizationWorker(
    ILogger<QuizFinalizationWorker> logger,
    IServiceProvider serviceProvider
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("QuizFinalizationWorker starting test execution...");

        using (var scope = serviceProvider.CreateScope())
        {
            logger.LogInformation("Resolving services within scope...");
            var userFactory = scope.ServiceProvider.GetRequiredService<UserFactory>();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            var user = userFactory.CreateUser("rabbit", "rabbit@gmail.com", "test", "Test");
            await userRepository.InsertUserAsync(user, cancellationToken);
        }
    }
}