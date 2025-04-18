using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuizSessionSeeder(
    IQuizSessionRepository quizSessionRepository,
    QuizSessionFactory quizSessionFactory,
    IUserRepository userRepository,
    IQuizRepository quizRepository,
    IQuestionRepository questionRepository)
{
    public async Task SeedQuizSessionsAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Quiz Sessions...");

        if ((await quizSessionRepository.GetAllQuizSessionsAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Quiz Sessions already seeded.");
            return;
        }

        var users = await userRepository.GetAllUsersAsync(cancellationToken);
        if (!users.Any())
        {
            Console.WriteLine("No Users found. Aborting Quiz Session seeding.");
            return;
        }

        var quizzes = await quizRepository.GetAllQuizzesAsync(cancellationToken);
        if (!quizzes.Any())
        {
            Console.WriteLine("No Quizzes found. Aborting Quiz Session seeding.");
            return;
        }

        var allQuestions = await questionRepository.GetAllQuestionsAsync(cancellationToken);
        var questionsByPackage = allQuestions.GroupBy(q => q.QuestionPackageId)
            .ToDictionary(g => g.Key, g => g.ToList());

        int count = 0;
        for (int i = 0; i < Math.Min(users.Count, quizzes.Count); i++)
        {
            var user = users[i];
            var quiz = quizzes[i];

            int totalQuestions = 0;
            if (questionsByPackage.TryGetValue(quiz.QuestionPackageId, out var questionsForQuiz))
            {
                totalQuestions = questionsForQuiz.Count;
            }
            else
            {
                Console.WriteLine(
                    $"Warning: Quiz {quiz.Id} references Package {quiz.QuestionPackageId} which has no questions. Skipping session creation.");
                continue;
            }

            if (totalQuestions == 0)
            {
                Console.WriteLine(
                    $"Warning: Quiz {quiz.Id} references Package {quiz.QuestionPackageId} which has 0 questions. Skipping session creation.");
                continue;
            }

            var quizSession = quizSessionFactory.CreateQuizSession(user.Id, quiz.Id, totalQuestions);

            await quizSessionRepository.InsertQuizSessionAsync(quizSession, cancellationToken);
            count++;
        }

        if (count == 0 && users.Any() && quizzes.Any())
        {
            var firstQuiz = quizzes.First();
            int totalQuestions = questionsByPackage.TryGetValue(firstQuiz.QuestionPackageId, out var questions)
                ? questions.Count
                : 0;

            if (totalQuestions > 0)
            {
                var quizSession = quizSessionFactory.CreateQuizSession(users.First().Id, firstQuiz.Id, totalQuestions);
                await quizSessionRepository.InsertQuizSessionAsync(quizSession, cancellationToken);
                count++;
            }
        }

        Console.WriteLine($"Seeded {count} Quiz Sessions.");
    }
}