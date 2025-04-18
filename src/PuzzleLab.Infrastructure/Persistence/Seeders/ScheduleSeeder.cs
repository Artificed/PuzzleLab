using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Seeders;

public class ScheduleSeeder(
    IScheduleRepository scheduleRepository,
    ScheduleFactory scheduleFactory)
{
    public async Task SeedSchedulesAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding Schedules...");

        if ((await scheduleRepository.GetAllSchedulesAsync(cancellationToken)).Any())
        {
            Console.WriteLine("Schedules already seeded.");
            return;
        }

        var schedulesToSeed = new List<(DateTime Start, DateTime End)>
        {
            (DateTime.UtcNow, DateTime.UtcNow.AddHours(1)),
            (DateTime.UtcNow.AddHours(2), DateTime.UtcNow.AddHours(3)),
            (DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(4)),
            (DateTime.UtcNow.AddMinutes(10), DateTime.UtcNow.AddHours(2)),
            (DateTime.UtcNow.AddMinutes(30), DateTime.UtcNow.AddMinutes(90))
        };

        for (int i = 0; i < schedulesToSeed.Count; i++)
        {
            var (start, end) = schedulesToSeed[i];
            var schedule = scheduleFactory.CreateSchedule(start, end);
            await scheduleRepository.InsertScheduleAsync(schedule, cancellationToken);
        }

        Console.WriteLine($"Seeded limit, {schedulesToSeed.Count} Schedules.");
    }
}