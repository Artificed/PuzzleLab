using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class Answer
{
    [Key] public Guid Id { get; private set; }
    [Required] public Guid QuestionId { get; private set; }
    [Required] public string Text { get; private set; }
    [Required] public bool IsCorrect { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastModifiedAt { get; private set; }

    [ForeignKey("QuestionId")] public virtual Question Question { get; private set; }
    public virtual ICollection<QuizAnswer> QuizAnswers { get; private set; }

    private Answer()
    {
    }

    internal Answer(Guid id, Guid questionId, string text, bool isCorrect)
    {
        Id = id;
        QuestionId = questionId;
        Text = text;
        IsCorrect = isCorrect;
        CreatedAt = DateTime.UtcNow.AddHours(7);
        LastModifiedAt = CreatedAt;
        QuizAnswers = new List<QuizAnswer>();
    }

    public void UpdateText(string newText)
    {
        if (string.IsNullOrWhiteSpace(newText))
            throw new ArgumentException("Answer text cannot be empty", nameof(newText));

        Text = newText;
        LastModifiedAt = DateTime.UtcNow.AddHours(7);
    }

    public void SetCorrect(bool correct)
    {
        IsCorrect = correct;
        LastModifiedAt = DateTime.UtcNow.AddHours(7);
    }
}