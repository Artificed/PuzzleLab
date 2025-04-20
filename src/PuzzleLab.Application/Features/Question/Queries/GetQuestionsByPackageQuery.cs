using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.Question.Responses;

namespace PuzzleLab.Application.Features.Question.Queries;

public record GetQuestionsByPackageId(Guid PackageId) : IRequest<Result<GetQuestionsByPackageResponse>>;