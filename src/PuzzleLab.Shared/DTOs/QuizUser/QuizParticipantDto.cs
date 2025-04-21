namespace PuzzleLab.Shared.DTOs.QuizUser;

public record QuizParticipantDto(Guid Id, Guid QuizId, Guid UserId, string Username);