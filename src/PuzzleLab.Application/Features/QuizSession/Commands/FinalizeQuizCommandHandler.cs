using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSession;
using PuzzleLab.Shared.DTOs.QuizSession.Requests;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Commands;

public class FinalizeQuizCommandHandler(
    IQuizSessionRepository quizSessionRepository
) : IRequestHandler<FinalizeQuizCommand, Result<FinalizeQuizResponse>>
{
    public async Task<Result<FinalizeQuizResponse>> Handle(FinalizeQuizCommand request,
        CancellationToken cancellationToken)
    {
        var session = await quizSessionRepository.GetQuizSessionByIdAsync(request.SessionId, cancellationToken);

        if (session is null)
        {
            return Result<FinalizeQuizResponse>.Failure(Error.NotFound("Quiz session not found!"));
        }

        if (session.FinalizedAt is not null)
        {
            return Result<FinalizeQuizResponse>.Failure(Error.Validation("Quiz session already finalized!"));
        }

        session.Finalize();
        await quizSessionRepository.UpdateQuizSessionAsync(session, cancellationToken);

        var responseData = new QuizSessionDto()
        {
            Id = session.Id,
            QuizId = session.QuizId,
            UserId = session.UserId,
            StartedAt = session.StartedAt,
            FinalizedAt = session.FinalizedAt,
            Status = session.Status
        };

        return Result<FinalizeQuizResponse>.Success(new FinalizeQuizResponse(responseData));
    }
}