using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Infrastructure.Persistence;

public class DatabaseContext(DbContextOptions<DatabaseContext> options): DbContext(options)
{
    public DbSet<User> Users{ get; set; }
}