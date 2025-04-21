namespace PuzzleLab.Shared.DTOs.QuizSession.Requests;

public class CreateQuizSessionRequest
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
}