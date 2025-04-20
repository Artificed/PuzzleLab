using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;

namespace PuzzleLab.Application.Features.QuestionPackage.Commands;

public class DeleteQuestionPackageCommandHandler(IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<DeleteQuestionPackageCommand, Result<DeleteQuestionPackageResponse>>
{
    public async Task<Result<DeleteQuestionPackageResponse>> Handle(DeleteQuestionPackageCommand request,
        CancellationToken cancellationToken)
    {
        var questionPackage =
            await questionPackageRepository.GetQuestionPackageByIdAsync(request.Id, cancellationToken);
        if (questionPackage is null)
        {
            return Result<DeleteQuestionPackageResponse>.Failure(Error.NotFound("Question Package not found"));
        }

        await questionPackageRepository.DeleteQuestionPackageAsync(questionPackage, cancellationToken);

        return Result<DeleteQuestionPackageResponse>.Success(new DeleteQuestionPackageResponse(questionPackage.Name));
    }
}