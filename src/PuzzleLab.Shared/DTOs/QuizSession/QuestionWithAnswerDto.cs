using PuzzleLab.Shared.DTOs.Answer;

namespace PuzzleLab.Shared.DTOs.QuizSession;

public record QuestionWithAnswerDto(
    Guid Id,
    string Text,
    byte[]? ImageData,
    string? ImageMimeType,
    string[] Answers
);