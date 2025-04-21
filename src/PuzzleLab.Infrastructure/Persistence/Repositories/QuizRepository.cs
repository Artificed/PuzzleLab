using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuizRepository(DatabaseContext databaseContext) : IQuizRepository
{
    public async Task<List<Quiz>> GetAllQuizzesAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.Quizzes.Include(q => q.QuizUsers).ToListAsync(cancellationToken);
    }

    public async Task<Quiz?> GetQuizByIdAsync(Guid quizId, CancellationToken cancellationToken = default)
    {
        return await databaseContext.Quizzes.Include(q => q.QuestionPackage).Include(q => q.QuizUsers)
            .FirstOrDefaultAsync(x => x.Id == quizId, cancellationToken);
    }

    public async Task InsertQuizAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        await databaseContext.Quizzes.AddAsync(quiz, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateQuizAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        databaseContext.Quizzes.Update(quiz);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteQuizAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        databaseContext.Quizzes.Remove(quiz);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}