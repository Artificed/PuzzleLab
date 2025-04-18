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

    public async Task<Answer?> GetAnswerByIdAsync(Guid answerId, CancellationToken cancellationToken = default)
    {
        return await databaseContext.Answers.FirstOrDefaultAsync(x => x.Id == answerId, cancellationToken);
    }

    public async Task InsertAnswerAsync(Answer answer, CancellationToken cancellationToken = default)
    {
        await databaseContext.Answers.AddAsync(answer, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}