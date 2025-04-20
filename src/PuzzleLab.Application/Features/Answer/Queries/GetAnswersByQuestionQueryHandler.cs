using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Answer;
using PuzzleLab.Shared.DTOs.Answer.Responses;

namespace PuzzleLab.Application.Features.Answer.Queries;

public class GetAnswersByQuestionQueryHandler(IAnswerRepository answerRepository)
    : IRequestHandler<GetAnswersByQuestionQuery, Result<GetAnswersByQuestionResponse>>
{
    public async Task<Result<GetAnswersByQuestionResponse>> Handle(
        GetAnswersByQuestionQuery request,
        CancellationToken cancellationToken)
    {
        var result = await answerRepository.GetAnswersByQuestionIdAsync(request.QuestionId, cancellationToken);

        var answers = result.Select(a => new AnswerDto()
        {
            Id = a.Id,
            QuestionId = a.QuestionId,
            Text = a.Text,
            IsCorrect = a.IsCorrect
        }).ToList();

        return Result<GetAnswersByQuestionResponse>.Success(new GetAnswersByQuestionResponse(answers));
    }
}