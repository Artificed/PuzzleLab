using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class Quiz
{
    [Key] public Guid Id { get; private set; }
    [Required] public Guid QuestionPackageId { get; set; }
    [Required] public Guid ScheduleId { get; private set; }
    [Required] public DateTime CreatedAt { get; private set; }
    [Required] public DateTime LastModifiedAt { get; private set; }

    [ForeignKey("QuestionPackageId")] public virtual QuestionPackage QuestionPackage { get; private set; }

    [ForeignKey("ScheduleId")] public virtual Schedule Schedule { get; private set; }
    public virtual ICollection<QuizUser> QuizUsers { get; private set; }
    public virtual ICollection<QuizSession> QuizSessions { get; private set; }

    private Quiz()
    {
    }

    internal Quiz(Guid id, Guid questionPackageId, Guid scheduleId)
    {
        Id = id;
        QuestionPackageId = questionPackageId;
        ScheduleId = scheduleId;
        CreatedAt = DateTime.UtcNow.AddHours(7);
        LastModifiedAt = CreatedAt;
        QuizUsers = new List<QuizUser>();
        QuizSessions = new List<QuizSession>();
    }

    public void UpdateQuestionPackageId(Guid questionPackageId)
    {
        QuestionPackageId = questionPackageId;
        LastModifiedAt = DateTime.UtcNow.AddHours(7);
    }

    public void AssignSchedule(Guid scheduleId)
    {
        ScheduleId = scheduleId;
        LastModifiedAt = DateTime.UtcNow.AddHours(7);
    }
}