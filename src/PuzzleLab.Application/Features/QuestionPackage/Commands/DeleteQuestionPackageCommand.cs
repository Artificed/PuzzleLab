using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;

namespace PuzzleLab.Application.Features.QuestionPackage.Commands;

public record DeleteQuestionPackageCommand(Guid Id) : IRequest<Result<DeleteQuestionPackageResponse>>;