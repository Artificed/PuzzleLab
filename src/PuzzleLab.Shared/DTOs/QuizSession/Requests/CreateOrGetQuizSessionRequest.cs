namespace PuzzleLab.Shared.DTOs.QuizSession.Requests;

public class CreateOrGetQuizSessionRequest
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
}