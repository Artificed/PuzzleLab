namespace PuzzleLab.Shared.DTOs.QuizAnswer.Requests;

public class SaveQuizAnswerRequest
{
    public Guid SessionId { get; set; }
    public Guid QuestionId { get; set; }
    public string Answer { get; set; }
}