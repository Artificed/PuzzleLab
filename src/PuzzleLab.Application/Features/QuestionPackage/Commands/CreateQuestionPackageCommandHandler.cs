using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuestionPackage;
using PuzzleLab.Shared.DTOs.QuestionPackage.Responses;

namespace PuzzleLab.Application.Features.QuestionPackage.Commands;

public class CreateQuestionPackageCommandHandler(IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<CreateQuestionPackageCommand, Result<CreateQuestionPackageResponse>>
{
    private readonly QuestionPackageFactory _questionPackageFactory = new();

    public async Task<Result<CreateQuestionPackageResponse>> Handle(CreateQuestionPackageCommand request,
        CancellationToken cancellationToken)
    {
        var newQuestionPackage = _questionPackageFactory.CreateQuestionPackage(request.Name, request.Description);

        await questionPackageRepository.InsertQuestionPackageAsync(newQuestionPackage, cancellationToken);

        var questionPackageDto = new QuestionPackageDto(
            newQuestionPackage.Id,
            newQuestionPackage.Name,
            newQuestionPackage.Description,
            0,
            newQuestionPackage.CreatedAt,
            newQuestionPackage.LastModifiedAt
        );

        return Result<CreateQuestionPackageResponse>.Success(new CreateQuestionPackageResponse(questionPackageDto));
    }
}