using PuzzleLab.Domain.Common;

namespace PuzzleLab.Domain.Events;

public record QuizFinalizedEvent(Guid SessionId) : IDomainEvent;