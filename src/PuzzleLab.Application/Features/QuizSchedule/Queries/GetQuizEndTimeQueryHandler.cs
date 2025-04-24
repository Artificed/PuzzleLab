using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Queries;

public class GetQuizEndTimeQueryHandler(IQuizRepository quizRepository)
    : IRequestHandler<GetQuizEndTimeQuery, Result<GetQuizEndTimeResponse>>
{
    public async Task<Result<GetQuizEndTimeResponse>> Handle(GetQuizEndTimeQuery request,
        CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.GetQuizByIdAsync(request.QuizId, cancellationToken);
        if (quiz == null)
        {
            return Result<GetQuizEndTimeResponse>.Failure(Error.NotFound("Quiz not found!"));
        }

        var endTime = quiz.Schedule.EndDateTime;
        return Result<GetQuizEndTimeResponse>.Success(new GetQuizEndTimeResponse(endTime));
    }
}