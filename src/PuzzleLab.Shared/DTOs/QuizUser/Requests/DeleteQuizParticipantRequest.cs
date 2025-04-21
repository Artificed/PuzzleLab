namespace PuzzleLab.Shared.DTOs.QuizUser.Requests;

public class DeleteQuizParticipantRequest
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
}