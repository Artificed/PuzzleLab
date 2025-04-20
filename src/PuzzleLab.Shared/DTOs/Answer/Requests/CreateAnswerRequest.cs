namespace PuzzleLab.Shared.DTOs.Answer.Requests;

public class CreateAnswerRequest
{
    public Guid QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}