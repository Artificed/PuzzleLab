using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class QuizUser
{
    [Key] public Guid Id { get; private set; }
    [Required] public Guid UserId { get; private set; }
    [Required] public Guid QuizId { get; private set; }
    [Required] public DateTime CreatedAt { get; private set; }
    [Required] public DateTime LastModifiedAt { get; private set; }

    [ForeignKey("UserId")] public virtual User User { get; private set; }
    [ForeignKey("QuizId")] public virtual Quiz Quiz { get; private set; }

    private QuizUser()
    {
    }

    internal QuizUser(Guid id, Guid userId, Guid quizId)
    {
        Id = id;
        UserId = userId;
        QuizId = quizId;
        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = CreatedAt;
    }
}