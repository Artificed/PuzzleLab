using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuestionRepository(DatabaseContext databaseContext) : IQuestionRepository
{
    public async Task<List<Question>> GetAllQuestionsAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.Questions.ToListAsync(cancellationToken);
    }

    public async Task<List<Question>> GetQuestionsByPackageIdAsync(Guid packageId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.Questions
            .Where(q => q.QuestionPackageId == packageId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Question?> GetQuestionByIdAsync(Guid questionId, CancellationToken cancellationToken = default)
    {
        var question = await databaseContext.Questions.FirstOrDefaultAsync(x => x.Id == questionId, cancellationToken);
        return question;
    }

    public async Task InsertQuestionAsync(Question question, CancellationToken cancellationToken = default)
    {
        await databaseContext.Questions.AddAsync(question, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateQuestionAsync(Question question, CancellationToken cancellationToken = default)
    {
        databaseContext.Questions.Update(question);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteQuestionAsync(Question question, CancellationToken cancellationToken = default)
    {
        databaseContext.Questions.Remove(question);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}