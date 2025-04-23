using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class QuizAnswer
{
    [Key] public Guid Id { get; private set; }
    [Required] public Guid QuizSessionId { get; private set; }
    [Required] public Guid QuestionId { get; private set; }
    [Required] public Guid SelectedAnswerId { get; private set; }
    [Required] public bool IsCorrect { get; private set; }
    [Required] public DateTime CreatedAt { get; private set; }
    [Required] public DateTime LastModifiedAt { get; private set; }

    [ForeignKey("QuizSessionId")] public virtual QuizSession QuizSession { get; private set; }
    [ForeignKey("QuestionId")] public virtual Question Question { get; private set; }
    [ForeignKey("SelectedAnswerId")] public virtual Answer Answer { get; private set; }

    private QuizAnswer()
    {
    }

    internal QuizAnswer(Guid id, Guid quizSessionId, Guid questionId, Guid selectedAnswerId, bool isCorrect)
    {
        Id = id;
        QuizSessionId = quizSessionId;
        QuestionId = questionId;
        SelectedAnswerId = selectedAnswerId;
        IsCorrect = isCorrect;
        CreatedAt = DateTime.UtcNow.AddHours(7);
        LastModifiedAt = CreatedAt;
    }

    public void UpdateSelectedAnswer(Guid selectedAnswerId, bool isCorrect)
    {
        SelectedAnswerId = selectedAnswerId;
        IsCorrect = isCorrect;
        LastModifiedAt = DateTime.UtcNow.AddHours(7);
    }
}