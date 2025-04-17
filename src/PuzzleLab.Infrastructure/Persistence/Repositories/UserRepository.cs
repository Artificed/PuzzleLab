using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class UserRepository(DatabaseContext databaseContext) : IUserRepository
{
    public Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return databaseContext.Users.ToListAsync(cancellationToken);
    }

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        databaseContext.Users.Add(user);
        return databaseContext.SaveChangesAsync(cancellationToken);
    }
}