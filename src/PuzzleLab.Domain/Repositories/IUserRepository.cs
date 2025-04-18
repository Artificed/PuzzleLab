using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IUserRepository
{
    Task InsertUserAsync(User user, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}