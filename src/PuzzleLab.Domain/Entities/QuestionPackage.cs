using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class QuestionPackage
{
    [Key] public Guid Id { get; private set; }
    [Required] public string Name { get; private set; }
    [Required] public string Description { get; private set; }
    [Required] public DateTime CreatedAt { get; private set; }
    [Required] public DateTime LastModifiedAt { get; private set; }
    public virtual ICollection<Question> Questions { get; private set; }
    public virtual ICollection<Quiz> Quizzes { get; private set; }

    private QuestionPackage()
    {
    }

    internal QuestionPackage(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = CreatedAt;
        Questions = new List<Question>();
        Quizzes = new List<Quiz>();
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Name cannot be empty", nameof(newName));

        Name = newName;
        LastModifiedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
        LastModifiedAt = DateTime.UtcNow;
    }
}