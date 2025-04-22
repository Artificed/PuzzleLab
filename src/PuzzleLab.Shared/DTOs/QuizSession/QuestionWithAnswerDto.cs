using PuzzleLab.Shared.DTOs.Answer;

namespace PuzzleLab.Shared.DTOs.QuizSession;

public class QuestionWithAnswerDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public byte[]? ImageData { get; set; }
    public string? ImageMimeType { get; set; }
    public AnswerOptionDto[] Answers { get; set; } = Array.Empty<AnswerOptionDto>();
}