using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
}