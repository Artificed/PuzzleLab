using PuzzleLab.Domain.Factories;
using PuzzleLab.Infrastructure.Persistence.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuizSeeder(
    DatabaseContext databaseContext)
{
    private readonly QuizRepository _quizRepository = new(databaseContext);
    private readonly QuizFactory _quizFactory = new();
    private readonly QuestionPackageRepository _packageRepository = new(databaseContext);
    private readonly ScheduleRepository _scheduleRepository = new(databaseContext);


    public async Task SeedQuizzesAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Quizzes...");

        if ((await _quizRepository.GetAllQuizzesAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Quizzes already seeded.");
            return;
        }

        var packages = await _packageRepository.GetAllQuestionPackagesAsync(cancellationToken);
        if (!packages.Any())
        {
            Console.WriteLine("No Question Packages found. Aborting Quiz seeding.");
            return;
        }

        var schedules = await _scheduleRepository.GetAllSchedulesAsync(cancellationToken);
        var scheduleIds = schedules.Select(s => (Guid)s.Id).ToList();

        int count = 0;
        for (int i = 0; i < packages.Count; i++)
        {
            var package = packages[i];
            Guid scheduleId;

            if (scheduleIds.Any() && i < scheduleIds.Count)
            {
                scheduleId = scheduleIds[i];
            }
            else
            {
                break;
            }

            var quiz = _quizFactory.CreateQuiz(package.Id, scheduleId);
            await _quizRepository.InsertQuizAsync(quiz, cancellationToken);
            count++;
        }

        Console.WriteLine($"Seeded {count} Quizzes.");
    }
}