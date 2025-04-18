using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class QuizSeeder(
    IQuizRepository quizRepository,
    QuizFactory quizFactory,
    IQuestionPackageRepository packageRepository,
    IScheduleRepository scheduleRepository)
{
    public async Task SeedQuizzesAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Quizzes...");

        if ((await quizRepository.GetAllQuizzesAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Quizzes already seeded.");
            return;
        }

        var packages = await packageRepository.GetAllQuestionPackagesAsync(cancellationToken);
        if (!packages.Any())
        {
            Console.WriteLine("No Question Packages found. Aborting Quiz seeding.");
            return;
        }

        var schedules = await scheduleRepository.GetAllSchedulesAsync(cancellationToken);
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

            var quiz = quizFactory.CreateQuiz(package.Id, scheduleId);
            await quizRepository.InsertQuizAsync(quiz, cancellationToken);
            count++;
        }

        Console.WriteLine($"Seeded {count} Quizzes.");
    }
}