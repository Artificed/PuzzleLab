using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Question;
using PuzzleLab.Shared.DTOs.Question.Responses;

namespace PuzzleLab.Application.Features.Question.Commands;

public class CreateQuestionCommandHandler(
    IQuestionRepository questionRepository,
    IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<CreateQuestionCommand, Result<CreateQuestionResponse>>
{
    private readonly QuestionFactory _questionFactory = new();

    public async Task<Result<CreateQuestionResponse>> Handle(CreateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var questionPackage =
            await questionPackageRepository.GetQuestionPackageByIdAsync(request.QuestionPackageId, cancellationToken);
        if (questionPackage is null)
        {
            return Result<CreateQuestionResponse>.Failure(
                Error.NotFound($"Question Package with id '{request.QuestionPackageId}' not found."));
        }

        var newQuestion = _questionFactory.CreateQuestionWithImage(
            questionPackage.Id,
            request.Text,
            request.ImageData,
            request.ImageMimeType);

        await questionRepository.InsertQuestionAsync(newQuestion, cancellationToken);

        var questionDto = new QuestionDto(
            newQuestion.Id,
            newQuestion.QuestionPackage.Id,
            newQuestion.Text,
            newQuestion.ImageData,
            newQuestion.ImageMimeType,
            newQuestion.CreatedAt,
            newQuestion.LastModifiedAt
        );

        return Result<CreateQuestionResponse>.Success(new CreateQuestionResponse(questionDto));
    }
}