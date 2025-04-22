using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuizSessionRepository
{
    Task InsertQuizSessionAsync(QuizSession quizSession, CancellationToken cancellationToken = default);
    Task<List<QuizSession>> GetAllQuizSessionsAsync(CancellationToken cancellationToken = default);
    Task<QuizSession?> GetQuizSessionByIdAsync(Guid quizSessionId, CancellationToken cancellationToken = default);

    Task<QuizSession?> GetExistingQuizSessionAsync(Guid quizId, Guid userId,
        CancellationToken cancellationToken = default);

    Task UpdateQuizSessionAsync(QuizSession quizSession, CancellationToken cancellationToken = default);
}