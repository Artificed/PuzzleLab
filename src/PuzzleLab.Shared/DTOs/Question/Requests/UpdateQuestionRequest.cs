namespace PuzzleLab.Shared.DTOs.Question.Requests;

public class UpdateQuestionRequest
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public byte[]? ImageData { get; set; }
    public string? ImageMimeType { get; set; }
}