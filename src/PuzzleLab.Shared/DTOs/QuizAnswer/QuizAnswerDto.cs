namespace PuzzleLab.Shared.DTOs.QuizAnswer;

public class QuizAnswerDto
{
    public Guid SessionId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid SelectedAnswerId { get; set; }
}