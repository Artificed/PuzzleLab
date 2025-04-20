using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;
using PuzzleLab.Shared.DTOs.QuestionPackage;

namespace PuzzleLab.Application.Features.QuestionPackage.Queries;

public class GetAllQuestionPackagesQueryHandler(IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<GetAllQuestionPackagesQuery, Result<GetAllQuestionPackagesResponse>>
{
    public async Task<Result<GetAllQuestionPackagesResponse>> Handle(GetAllQuestionPackagesQuery request,
        CancellationToken cancellationToken)
    {
        var questionPackages = await questionPackageRepository.GetAllQuestionPackagesAsync(cancellationToken);

        var questionPackageDtos = questionPackages.Select(qp => new QuestionPackageDto(
            qp.Id,
            qp.Name,
            qp.Description,
            qp.Questions.Count,
            qp.CreatedAt,
            qp.LastModifiedAt
        )).ToList();

        return Result<GetAllQuestionPackagesResponse>.Success(new GetAllQuestionPackagesResponse(questionPackageDtos));
    }
}