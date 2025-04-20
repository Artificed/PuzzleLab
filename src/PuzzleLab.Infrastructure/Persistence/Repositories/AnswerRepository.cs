using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class AnswerRepository(DatabaseContext databaseContext) : IAnswerRepository
{
    public async Task<List<Answer>> GetAllAnswersAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.Answers.ToListAsync(cancellationToken);
    }

    public async Task ClearAnswersByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken = default)
    {
        var answers = await databaseContext.Answers
            .Where(x => x.QuestionId == questionId)
            .ToListAsync(cancellationToken);

        databaseContext.Answers.RemoveRange(answers);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Answer>> GetAnswersByQuestionIdAsync(Guid questionId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.Answers
            .Where(x => x.QuestionId == questionId)
            .ToListAsync(cancellationToken);
    }

    public async Task InsertAnswerAsync(Answer answer, CancellationToken cancellationToken = default)
    {
        await databaseContext.Answers.AddAsync(answer, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}