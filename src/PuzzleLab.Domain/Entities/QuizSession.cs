using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PuzzleLab.Domain.Common;
using PuzzleLab.Domain.Events;

namespace PuzzleLab.Domain.Entities;

public class QuizSession
{
    [Key] public Guid Id { get; private set; }
    [Required] public Guid UserId { get; private set; }
    [Required] public Guid QuizId { get; private set; }
    [Required] public DateTime StartedAt { get; private set; }
    public DateTime? FinalizedAt { get; private set; }
    public string Status { get; private set; }
    public int CorrectAnswers { get; private set; }
    public int TotalQuestions { get; private set; }
    [Required] public DateTime CreatedAt { get; private set; }
    [Required] public DateTime LastModifiedAt { get; private set; }

    [ForeignKey("UserId")] public virtual User User { get; private set; }
    [ForeignKey("QuizId")] public virtual Quiz Quiz { get; private set; }

    public virtual ICollection<QuizAnswer> QuizAnswers { get; private set; }
    public virtual ICollection<QuizSessionQuestion> QuizSessionQuestions { get; private set; }

    private QuizSession()
    {
    }

    internal QuizSession(Guid id, Guid userId, Guid quizId, int totalQuestions)
    {
        Id = id;
        UserId = userId;
        QuizId = quizId;
        StartedAt = DateTime.UtcNow.AddHours(7);
        Status = "In Progress";
        CorrectAnswers = 0;
        TotalQuestions = totalQuestions;
        CreatedAt = DateTime.UtcNow.AddHours(7);
        LastModifiedAt = CreatedAt;
        QuizAnswers = new List<QuizAnswer>();
        QuizSessionQuestions = new List<QuizSessionQuestion>();
    }

    public void Finalize(int correctAnswers)
    {
        Status = "Finalized";
        CorrectAnswers = correctAnswers;
        FinalizedAt = DateTime.UtcNow.AddHours(7);
        LastModifiedAt = DateTime.UtcNow.AddHours(7);
    }
}