namespace PuzzleLab.Shared.DTOs.QuizUser.Requests;

public class CreateQuizParticipantRequest
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
}