using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IUserRepository
{
    Task InsertUserAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<List<User>> GetAvailableUserForQuizAsync(Guid quizId, CancellationToken cancellationToken = default);
}