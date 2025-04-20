using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;

namespace PuzzleLab.Application.Features.QuestionPackage.Queries;

public record GetQuestionPackageByIdQuery(Guid Id) : IRequest<Result<GetQuestionPackageByIdResponse>>;