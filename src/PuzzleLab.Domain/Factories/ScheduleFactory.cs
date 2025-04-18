using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class ScheduleFactory
{
    public Schedule CreateSchedule(DateTime startDateTime, DateTime endDateTime)
    {
        return new Schedule(
            Guid.NewGuid(),
            startDateTime,
            endDateTime
        );
    }
}