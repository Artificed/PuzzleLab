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

    public Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        return databaseContext.Users
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}