using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuestionPackageRepository(DatabaseContext databaseContext) : IQuestionPackageRepository
{
    public async Task<List<QuestionPackage>> GetAllQuestionPackagesAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuestionPackages.Include(qp => qp.Questions).ToListAsync(cancellationToken);
    }

    public async Task<QuestionPackage?> GetQuestionPackageByIdAsync(Guid packageId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuestionPackages.Include(qp => qp.Questions)
            .FirstOrDefaultAsync(x => x.Id == packageId, cancellationToken);
    }

    public async Task InsertQuestionPackageAsync(QuestionPackage package, CancellationToken cancellationToken = default)
    {
        await databaseContext.QuestionPackages.AddAsync(package, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateQuestionPackageAsync(QuestionPackage package, CancellationToken cancellationToken = default)
    {
        databaseContext.QuestionPackages.Update(package);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteQuestionPackageAsync(QuestionPackage package, CancellationToken cancellationToken = default)
    {
        databaseContext.QuestionPackages.Remove(package);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}