using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuzzleLab.Domain.Entities;

public class QuestionPackage
{
    [Key]
    public Guid Id { get; private set; }

    [Required]
    public string Name { get; private set; }

    [Required]
    public string Description { get; private set; }

    [Required]
    public int DurationInMinutes { get; private set; }

    [Required]
    public byte[] ImageData { get; private set; }

    [Required]
    public Guid CreatedById { get; private set; }

    [Required]
    public DateTime CreatedAt { get; private set; }

    [Required]
    public DateTime LastModifiedAt { get; private set; }

    [ForeignKey(nameof(CreatedById))]
    public virtual User CreatedBy { get; private set; }

    internal QuestionPackage(Guid id, string name, string description, int durationInMinutes, Guid createdById)
    {
        Id = id;
        Name = name;
        Description = description;
        DurationInMinutes = durationInMinutes;
        CreatedById = createdById;
        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = CreatedAt;
    }

    public void UpdateName(string newName)
    {
        Name = newName;
        LastModifiedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
        LastModifiedAt = DateTime.UtcNow;
    }

    public void UpdateDuration(int newDurationInMinutes)
    {
        DurationInMinutes = newDurationInMinutes;
        LastModifiedAt = DateTime.UtcNow;
    }

    public void UpdateImage(byte[] newImageData)
    {
        ImageData = newImageData;
        LastModifiedAt = DateTime.UtcNow;
    }
}
