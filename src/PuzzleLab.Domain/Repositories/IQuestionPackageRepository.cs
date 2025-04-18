using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IQuestionPackageRepository
{
    Task<List<QuestionPackage>> GetAllQuestionPackagesAsync(CancellationToken cancellationToken = default);
    Task<QuestionPackage?> GetQuestionPackageById(Guid questionPackageId, CancellationToken cancellationToken = default);
    Task InsertQuestionPackageAsync(QuestionPackage questionPackage, CancellationToken cancellationToken = default);
}