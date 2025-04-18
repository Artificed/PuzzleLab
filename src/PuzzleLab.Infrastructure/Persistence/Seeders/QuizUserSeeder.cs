using PuzzleLab.Domain.Factories;
using PuzzleLab.Infrastructure.Persistence.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuizUserSeeder(DatabaseContext databaseContext)
{
    private readonly QuizUserRepository _quizUserRepository = new(databaseContext);
    private readonly QuizUserFactory _quizUserFactory = new();
    private readonly UserRepository _userRepository = new(databaseContext);
    private readonly QuizRepository _quizRepository = new(databaseContext);

    public async Task SeedQuizUsersAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Quiz Users (Assignments)...");

        if ((await _quizUserRepository.GetAllQuizUsersAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Quiz Users already seeded.");
            return;
        }

        var users = await _userRepository.GetAllUsersAsync(cancellationToken);
        if (!users.Any())
        {
            Console.WriteLine("No Users found. Aborting Quiz User seeding.");
            return;
        }

        var quizzes = await _quizRepository.GetAllQuizzesAsync(cancellationToken);
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
                var quizUser = _quizUserFactory.CreateQuizUser(user.Id, quiz.Id);
                await _quizUserRepository.InsertQuizUserAsync(quizUser, cancellationToken);
                count++;
            }
        }

        if (count == 0 && users.Any() && quizzes.Any())
        {
            var quizUser = _quizUserFactory.CreateQuizUser(users.First().Id, quizzes.First().Id);
            await _quizUserRepository.InsertQuizUserAsync(quizUser, cancellationToken);
            count++;
        }

        Console.WriteLine($"Seeded {count} Quiz User assignments.");
    }
}