using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuestionPackage;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;

namespace PuzzleLab.Application.Features.QuestionPackage.Commands;

public class UpdateQuestionPackageCommandHandler(IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<UpdateQuestionPackageCommand, Result<UpdateQuestionPackageResponse>>
{
    public async Task<Result<UpdateQuestionPackageResponse>> Handle(UpdateQuestionPackageCommand request,
        CancellationToken cancellationToken)
    {
        var questionPackage =
            await questionPackageRepository.GetQuestionPackageByIdAsync(request.Id, cancellationToken);
        if (questionPackage is null)
        {
            return Result<UpdateQuestionPackageResponse>.Failure(Error.NotFound("Question Package not found!"));
        }

        questionPackage.UpdateName(request.Name);
        questionPackage.UpdateDescription(request.Description);

        await questionPackageRepository.UpdateQuestionPackageAsync(questionPackage, cancellationToken);

        var questionPackageDto = new QuestionPackageDto(
            questionPackage.Id,
            questionPackage.Name,
            questionPackage.Description,
            questionPackage.Quizzes.Count,
            questionPackage.CreatedAt,
            questionPackage.LastModifiedAt
        );

        return Result<UpdateQuestionPackageResponse>.Success(new UpdateQuestionPackageResponse(questionPackageDto));
    }
}