using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class Schedule
{
    [Key] public Guid Id { get; private set; }

    [Required] public DateTime StartDateTime { get; private set; }

    [Required] public DateTime EndDateTime { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastModifiedAt { get; private set; }

    private Schedule()
    {
    }

    internal Schedule(Guid id, DateTime startDateTime,
        DateTime endDateTime)
    {
        if (endDateTime <= startDateTime)
            throw new ArgumentException("End time must be after start time");

        Id = id;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        CreatedAt = DateTime.UtcNow.AddHours(7);
        LastModifiedAt = CreatedAt;
    }

    public void UpdateTimeWindow(DateTime newStartDateTime, DateTime newEndDateTime)
    {
        if (newEndDateTime <= newStartDateTime)
            throw new ArgumentException("End time must be after start time");

        StartDateTime = newStartDateTime;
        EndDateTime = newEndDateTime;
        LastModifiedAt = DateTime.UtcNow.AddHours(7);
    }
}