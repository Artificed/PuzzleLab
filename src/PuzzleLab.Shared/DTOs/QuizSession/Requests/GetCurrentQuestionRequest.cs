namespace PuzzleLab.Shared.DTOs.QuizSession.Requests;

public class GetCurrentQuestionRequest
{
    public string QuizId { get; set; } = string.Empty;
    public int QuestionIndex { get; set; }
}