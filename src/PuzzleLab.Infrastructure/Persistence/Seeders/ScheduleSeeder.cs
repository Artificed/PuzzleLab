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
            (DateTime.UtcNow.AddHours(7).AddDays(2), DateTime.UtcNow.AddHours(7).AddDays(3)),
            (DateTime.UtcNow.AddHours(7), DateTime.UtcNow.AddHours(7).AddHours(13)),
            (DateTime.UtcNow.AddHours(7), DateTime.UtcNow.AddHours(7).AddSeconds(14)),
            (DateTime.UtcNow.AddHours(7), DateTime.UtcNow.AddHours(7).AddHours(9)),
            (DateTime.UtcNow.AddHours(7).AddHours(12), DateTime.UtcNow.AddHours(7).AddHours(14)),
            (DateTime.UtcNow.AddHours(7).AddMinutes(10), DateTime.UtcNow.AddHours(7).AddHours(2)),
            (DateTime.UtcNow.AddHours(7).AddMinutes(30), DateTime.UtcNow.AddHours(7).AddMinutes(70)),
            (DateTime.UtcNow.AddHours(7).AddMinutes(10), DateTime.UtcNow.AddHours(7).AddHours(3)),
            (DateTime.UtcNow.AddHours(7).AddMinutes(30), DateTime.UtcNow.AddHours(7).AddMinutes(40))
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