using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuestionPackage;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;

namespace PuzzleLab.Application.Features.QuestionPackage.Queries;

public class GetQuestionPackageByIdQueryHandler(IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<GetQuestionPackageByIdQuery, Result<GetQuestionPackageByIdResponse>>
{
    public async Task<Result<GetQuestionPackageByIdResponse>> Handle(GetQuestionPackageByIdQuery request,
        CancellationToken cancellationToken)
    {
        var questionPackage =
            await questionPackageRepository.GetQuestionPackageByIdAsync(request.Id, cancellationToken);
        if (questionPackage is null)
        {
            return Result<GetQuestionPackageByIdResponse>.Failure(Error.NotFound("Question Package not found!"));
        }

        var questionPackageDto = new QuestionPackageDto(
            questionPackage.Id,
            questionPackage.Name,
            questionPackage.Description,
            questionPackage.Questions.Count,
            questionPackage.CreatedAt,
            questionPackage.LastModifiedAt
        );

        var response = new GetQuestionPackageByIdResponse(questionPackageDto);
        return Result<GetQuestionPackageByIdResponse>.Success(response);
    }
}