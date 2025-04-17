using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}