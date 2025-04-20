using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Question;
using PuzzleLab.Shared.DTOs.Question.Responses;

namespace PuzzleLab.Application.Features.Question.Commands;

public class UpdateQuestionCommandHandler(IQuestionRepository questionRepository)
    : IRequestHandler<UpdateQuestionCommand, Result<UpdateQuestionResponse>>
{
    public async Task<Result<UpdateQuestionResponse>> Handle(UpdateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var question = await questionRepository.GetQuestionByIdAsync(request.Id, cancellationToken);
        if (question is null)
        {
            return Result<UpdateQuestionResponse>.Failure(Error.NotFound("Question not found!"));
        }

        question.UpdateText(request.Text);

        await questionRepository.UpdateQuestionAsync(question, cancellationToken);

        var questionDto = new QuestionDto(
            question.Id,
            question.QuestionPackageId,
            question.Text,
            question.ImageData,
            question.ImageMimeType,
            question.CreatedAt,
            question.LastModifiedAt
        );

        return Result<UpdateQuestionResponse>.Success(new UpdateQuestionResponse(questionDto));
    }
}