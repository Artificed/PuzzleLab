using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuizSessionRepository
{
    Task AddQuizSessionAsync(QuizSession quizSession, CancellationToken cancellationToken = default);
    Task<List<QuizSession>> GetAllQuizSessionsAsync(CancellationToken cancellationToken = default);
    Task<QuizSession?> GetQuizSessionById(Guid quizSessionId, CancellationToken cancellationToken = default);
}