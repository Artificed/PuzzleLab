using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuizRepository(DatabaseContext databaseContext) : IQuizRepository
{
    public async Task<List<Quiz>> GetAllQuizzesAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.Quizzes.ToListAsync(cancellationToken);
    }

    public async Task<Quiz?> GetQuizByIdAsync(Guid quizId, CancellationToken cancellationToken = default)
    {
        return await databaseContext.Quizzes.FirstOrDefaultAsync(x => x.Id == quizId, cancellationToken);
    }

    public async Task InsertQuizAsync(Quiz quiz, CancellationToken cancellationToken = default)
    {
        await databaseContext.Quizzes.AddAsync(quiz, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}