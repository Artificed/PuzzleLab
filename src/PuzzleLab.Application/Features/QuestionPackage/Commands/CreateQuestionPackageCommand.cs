using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;

namespace PuzzleLab.Application.Features.QuestionPackage.Commands;

public record CreateQuestionPackageCommand(string Name, string Description)
    : IRequest<Result<CreateQuestionPackageResponse>>;