using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuizRepository
{
    Task InsertQuizAsync(Quiz quiz, CancellationToken cancellationToken = default);
    Task<List<Quiz>> GetAllQuizzesAsync(CancellationToken cancellationToken = default);
    Task<Quiz?> GetQuizByIdAsync(Guid quizId, CancellationToken cancellationToken = default);
    Task UpdateQuizAsync(Quiz quiz, CancellationToken cancellationToken = default);
    Task DeleteQuizAsync(Quiz quiz, CancellationToken cancellationToken = default);
}