using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuizUserSeeder(
    IQuizUserRepository quizUserRepository,
    QuizUserFactory quizUserFactory,
    IUserRepository userRepository,
    IQuizRepository quizRepository)
{
    public async Task SeedQuizUsersAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Quiz Users (Assignments)...");

        if ((await quizUserRepository.GetAllQuizUsersAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Quiz Users already seeded.");
            return;
        }

        var users = await userRepository.GetAllUsersAsync(cancellationToken);
        if (!users.Any())
        {
            Console.WriteLine("No Users found. Aborting Quiz User seeding.");
            return;
        }

        var quizzes = await quizRepository.GetAllQuizzesAsync(cancellationToken);
        if (!quizzes.Any())
        {
            Console.WriteLine("No Quizzes found. Aborting Quiz User seeding.");
            return;
        }

        int count = 0;
        int quizzesToAssign = Math.Min(quizzes.Count, 4);
        int usersToAssign = Math.Min(users.Count, 3);

        for (int u = 0; u < usersToAssign; u++)
        {
            var user = users[u];
            for (int q = 0; q < quizzesToAssign; q++)
            {
                var quiz = quizzes[q];
                var quizUser = quizUserFactory.CreateQuizUser(user.Id, quiz.Id);
                await quizUserRepository.InsertQuizUserAsync(quizUser, cancellationToken);
                count++;
            }
        }

        if (count == 0 && users.Any() && quizzes.Any())
        {
            var quizUser = quizUserFactory.CreateQuizUser(users.First().Id, quizzes.First().Id);
            await quizUserRepository.InsertQuizUserAsync(quizUser, cancellationToken);
            count++;
        }

        Console.WriteLine($"Seeded {count} Quiz User assignments.");
    }
}