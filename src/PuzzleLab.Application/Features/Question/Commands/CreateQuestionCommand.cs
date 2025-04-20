using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.Question.Responses;

namespace PuzzleLab.Application.Features.Question.Commands;

public record CreateQuestionCommand(Guid QuestionPackageId, string Text, byte[]? ImageData, string? ImageMimeType)
    : IRequest<Result<CreateQuestionResponse>>;