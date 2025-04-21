using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizUser;
using PuzzleLab.Shared.DTOs.QuizUser.Responses;

namespace PuzzleLab.Application.Features.QuizParticipant.Commands;

public class CreateQuizParticipantCommandHandler(
    IQuizUserRepository quizUserRepository,
    IUserRepository userRepository,
    IQuizRepository quizRepository)
    : IRequestHandler<CreateQuizParticipantCommand, Result<CreateQuizParticipantResponse>>
{
    private readonly QuizUserFactory _quizUserFactory = new();

    public async Task<Result<CreateQuizParticipantResponse>> Handle(CreateQuizParticipantCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result<CreateQuizParticipantResponse>.Failure(Error.NotFound("User not found!"));
        }

        var quiz = await quizRepository.GetQuizByIdAsync(request.QuizId, cancellationToken);
        if (quiz is null)
        {
            return Result<CreateQuizParticipantResponse>.Failure(Error.NotFound("Quiz not found!"));
        }

        var existingParticipant = await quizUserRepository.GetByUserIdAndQuizIdAsync(
            request.UserId,
            request.QuizId,
            cancellationToken
        );

        if (existingParticipant is not null)
        {
            return Result<CreateQuizParticipantResponse>.Failure(
                Error.Validation("User is already registered to this quiz!"));
        }

        var newQuizParticipant = _quizUserFactory.CreateQuizUser(request.UserId, request.QuizId);
        await quizUserRepository.InsertQuizUserAsync(newQuizParticipant, cancellationToken);

        var questionPackageDto = new QuizParticipantDto(
            newQuizParticipant.Id,
            newQuizParticipant.QuizId,
            newQuizParticipant.UserId,
            user.Username
        );

        return Result<CreateQuizParticipantResponse>.Success(new CreateQuizParticipantResponse(questionPackageDto));
    }
}