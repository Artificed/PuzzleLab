using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Question.Responses;

namespace PuzzleLab.Application.Features.Question.Commands;

public class DeleteQuestionCommandHandler(IQuestionRepository questionRepository)
    : IRequestHandler<DeleteQuestionCommand, Result<DeleteQuestionResponse>>
{
    public async Task<Result<DeleteQuestionResponse>> Handle(DeleteQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var question = await questionRepository.GetQuestionByIdAsync(request.Id, cancellationToken);
        if (question is null)
        {
            return Result<DeleteQuestionResponse>.Failure(Error.NotFound("Question not found"));
        }

        await questionRepository.DeleteQuestionAsync(question, cancellationToken);

        return Result<DeleteQuestionResponse>.Success(new DeleteQuestionResponse(question.Text));
    }
}