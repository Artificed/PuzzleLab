using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuizAnswerRepository(DatabaseContext databaseContext) : IQuizAnswerRepository
{
    public async Task<List<QuizAnswer>> GetAllQuizAnswersAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizAnswers.ToListAsync(cancellationToken);
    }

    public async Task<QuizAnswer?> GetQuizAnswerByIdAsync(Guid quizAnswerId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuizAnswers.FirstOrDefaultAsync(x => x.Id == quizAnswerId, cancellationToken);
    }

    public async Task InsertQuizAnswerAsync(QuizAnswer quizAnswer, CancellationToken cancellationToken = default)
    {
        await databaseContext.QuizAnswers.AddAsync(quizAnswer, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}