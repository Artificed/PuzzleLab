namespace PuzzleLab.Shared.DTOs.Question;

public record QuestionDto(
    Guid Id,
    Guid QuestionPackageId,
    string Text,
    byte[]? ImageData,
    string? ImageMimeType,
    DateTime CreatedAt,
    DateTime? LastModifiedAt
);