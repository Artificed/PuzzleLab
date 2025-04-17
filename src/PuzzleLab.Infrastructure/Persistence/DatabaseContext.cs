using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Infrastructure.Persistence;

public class DatabaseContext(DbContextOptions options): DbContext(options)
{
    public DbSet<User> Users{ get; set; }
}