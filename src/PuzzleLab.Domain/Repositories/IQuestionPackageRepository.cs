using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuestionPackageRepository
{
    Task<List<QuestionPackage>> GetAllQuestionPackagesAsync(CancellationToken cancellationToken = default);

    Task<QuestionPackage?> GetQuestionPackageByIdAsync(Guid questionPackageId,
        CancellationToken cancellationToken = default);

    Task InsertQuestionPackageAsync(QuestionPackage questionPackage, CancellationToken cancellationToken = default);
    Task UpdateQuestionPackageAsync(QuestionPackage questionPackage, CancellationToken cancellationToken = default);
    Task DeleteQuestionPackageAsync(QuestionPackage questionPackage, CancellationToken cancellationToken = default);
}