using PuzzleLab.Domain.Common;

namespace PuzzleLab.Domain.Events;

public record QuizFinalizedEvent(Guid SessionId, Guid QuizId, Guid UserId, DateTime FinalizedAt, int CorrectCount)
    : IDomainEvent;