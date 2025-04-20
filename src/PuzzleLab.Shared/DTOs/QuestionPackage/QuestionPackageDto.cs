namespace PuzzleLab.Shared.DTOs.QuestionPackage;

public class QuestionPackageDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastModifiedAt { get; private set; }
}