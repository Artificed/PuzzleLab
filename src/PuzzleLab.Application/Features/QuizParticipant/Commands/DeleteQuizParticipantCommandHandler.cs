using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizUser.Responses;

namespace PuzzleLab.Application.Features.QuizParticipant.Commands;

public class DeleteQuizParticipantCommandHandler(
    IQuizUserRepository quizUserRepository)
    : IRequestHandler<DeleteQuizParticipantCommand, Result<DeleteQuizParticipantResponse>>
{
    public async Task<Result<DeleteQuizParticipantResponse>> Handle(DeleteQuizParticipantCommand request,
        CancellationToken cancellationToken)
    {
        var existingParticipant =
            await quizUserRepository.GetByUserIdAndQuizIdAsync(request.UserId, request.QuizId, cancellationToken);

        if (existingParticipant is null)
        {
            return Result<DeleteQuizParticipantResponse>.Failure(Error.NotFound("Participant not found."));
        }

        await quizUserRepository.DeleteByIdAsync(existingParticipant.Id, cancellationToken);

        return Result<DeleteQuizParticipantResponse>.Success(
            new DeleteQuizParticipantResponse(existingParticipant.Id.ToString())
        );
    }
}