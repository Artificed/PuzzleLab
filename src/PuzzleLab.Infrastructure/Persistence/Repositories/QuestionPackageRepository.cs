using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Infrastructure.Persistence;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class QuestionPackageRepository(DatabaseContext databaseContext) : IQuestionPackageRepository
{
    public async Task<List<QuestionPackage>> GetAllQuestionPackagesAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuestionPackages.ToListAsync(cancellationToken);
    }

    public async Task<QuestionPackage?> GetQuestionPackageByIdAsync(Guid packageId,
        CancellationToken cancellationToken = default)
    {
        return await databaseContext.QuestionPackages.FirstOrDefaultAsync(x => x.Id == packageId, cancellationToken);
    }

    public async Task InsertQuestionPackageAsync(QuestionPackage package, CancellationToken cancellationToken = default)
    {
        await databaseContext.QuestionPackages.AddAsync(package, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}