namespace PuzzleLab.Shared.DTOs.Question;

public class QuestionDto
{
    public Guid Id { get; set; }
    public Guid QuestionPackageId { get; set; }
    public string Text { get; set; }
    public byte[]? ImageData { get; set; }
    public string? ImageMimeType { get; set; }
}