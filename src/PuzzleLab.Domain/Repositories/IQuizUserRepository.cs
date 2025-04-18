using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuizUserRepository
{
    Task InsertQuizUserAsync(QuizUser quizUser, CancellationToken cancellationToken = default);
    Task<List<QuizUser>> GetAllQuizUsersAsync(CancellationToken cancellationToken = default);
    Task<QuizUser?> GetQuizUserByIdAsync(Guid quizUserId, CancellationToken cancellationToken = default);
}