namespace PuzzleLab.Shared.DTOs.QuestionPackage;

public record QuestionPackageDto(
    Guid Id,
    string Name,
    string Description,
    Int32 QuestionsCount,
    DateTime CreatedAt,
    DateTime? LastModifiedAt);