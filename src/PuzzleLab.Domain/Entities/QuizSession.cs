using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        StartedAt = DateTime.UtcNow;
        Status = "In Progress";
        CorrectAnswers = 0;
        TotalQuestions = totalQuestions;
        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = CreatedAt;
        QuizAnswers = new List<QuizAnswer>();
        QuizSessionQuestions = new List<QuizSessionQuestion>();
    }

    public void Finalize()
    {
        FinalizedAt = DateTime.UtcNow;
        Status = "Finalized";
        LastModifiedAt = DateTime.UtcNow;
    }

    public void UpdateCorrectAnswers(int correctAnswers)
    {
        CorrectAnswers = correctAnswers;
        LastModifiedAt = DateTime.UtcNow;
    }
}