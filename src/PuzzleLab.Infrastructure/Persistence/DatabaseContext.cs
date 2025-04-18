using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Infrastructure.Persistence;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<QuestionPackage> QuestionPackages { get; set; }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }

    public DbSet<QuizUser> QuizUsers { get; set; }
    public DbSet<QuizSession> QuizSessions { get; set; }
    public DbSet<QuizAnswer> QuizAnswers { get; set; }
}