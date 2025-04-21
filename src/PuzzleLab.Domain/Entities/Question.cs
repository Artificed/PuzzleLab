using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class Question
{
    [Key] public Guid Id { get; private set; }

    [Required] public Guid QuestionPackageId { get; private set; }

    [Required] public string Text { get; private set; }

    public byte[]? ImageData { get; private set; }

    public string? ImageMimeType { get; private set; }

    [Required] public DateTime CreatedAt { get; private set; }

    [Required] public DateTime LastModifiedAt { get; private set; }

    [ForeignKey("QuestionPackageId")] public virtual QuestionPackage QuestionPackage { get; private set; }

    public virtual ICollection<Answer> Answers { get; private set; }
    public virtual ICollection<QuizAnswer> QuizAnswers { get; private set; }
    public virtual ICollection<QuizSessionQuestion> QuizSessionQuestions { get; private set; }

    private Question()
    {
    }

    internal Question(Guid id, Guid questionPackageId, string text)
    {
        Id = id;
        QuestionPackageId = questionPackageId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = CreatedAt;
        Answers = new List<Answer>();
        QuizAnswers = new List<QuizAnswer>();
        QuizSessionQuestions = new List<QuizSessionQuestion>();
    }

    public void UpdateText(string newText)
    {
        if (string.IsNullOrWhiteSpace(newText))
            throw new ArgumentException("Question text cannot be empty", nameof(newText));

        Text = newText;
        LastModifiedAt = DateTime.UtcNow;
    }

    public void UpdateImage(byte[]? newImageData, string? mimeType)
    {
        ImageData = newImageData;
        ImageMimeType = mimeType;
        LastModifiedAt = DateTime.UtcNow;
    }
}