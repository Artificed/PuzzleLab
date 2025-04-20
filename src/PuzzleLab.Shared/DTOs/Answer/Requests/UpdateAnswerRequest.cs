namespace PuzzleLab.Shared.DTOs.Answer.Requests;

public class UpdateAnswerRequest
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}