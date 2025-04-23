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

    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

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
        _events.Add(new QuizFinalizedEvent(Id, QuizId, UserId, FinalizedAt.Value, CorrectAnswers));
    }

    public void ClearEvents()
    {
        _events.Clear();
    }

    public void UpdateCorrectAnswers(int correctAnswers)
    {
        CorrectAnswers = correctAnswers;
        LastModifiedAt = DateTime.UtcNow;
    }
}