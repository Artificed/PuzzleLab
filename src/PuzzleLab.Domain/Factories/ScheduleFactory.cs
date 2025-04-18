using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class ScheduleFactory
{
    public Schedule CreateSchedule(string name, DateTime startDateTime, DateTime endDateTime, Guid questionPackageId,
        Guid createdById)
    {
        return new Schedule(
            Guid.NewGuid(),
            name,
            startDateTime,
            endDateTime,
            questionPackageId,
            createdById
        );
    }
}