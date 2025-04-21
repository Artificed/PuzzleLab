using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSession;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Commands;

public class CreateQuizSessionCommandHandler(
    IQuizRepository quizRepository,
    IUserRepository userRepository,
    IQuizUserRepository quizUserRepository,
    IScheduleRepository scheduleRepository,
    QuizSessionFactory quizSessionFactory,
    IQuizSessionRepository quizSessionRepository,
    IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<CreateQuizSessionCommand, Result<CreateQuizSessionResponse>>
{
    public async Task<Result<CreateQuizSessionResponse>> Handle(CreateQuizSessionCommand request,
        CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.GetQuizByIdAsync(request.QuizId, cancellationToken);
        if (quiz is null)
        {
            return Result<CreateQuizSessionResponse>.Failure(Error.NotFound("Quiz not found!"));
        }

        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result<CreateQuizSessionResponse>.Failure(Error.NotFound("User not found!"));
        }

        var quizUser = await quizUserRepository.GetByUserIdAndQuizIdAsync(user.Id, quiz.Id, cancellationToken);
        if (quizUser is null)
        {
            return Result<CreateQuizSessionResponse>.Failure(Error.NotFound("User is not registered for this quiz!"));
        }

        var quizSchedule = await scheduleRepository.GetScheduleByIdAsync(quiz.ScheduleId, cancellationToken);
        if (quizSchedule is null)
        {
            return Result<CreateQuizSessionResponse>.Failure(Error.NotFound("Schedule not found!"));
        }

        if (quizSchedule.StartDateTime > DateTime.Now)
        {
            return Result<CreateQuizSessionResponse>.Failure(
                Error.Validation("Quiz cannot be started before the schedule start time!"));
        }

        if (quizSchedule.EndDateTime < DateTime.Now)
        {
            return Result<CreateQuizSessionResponse>.Failure(
                Error.Validation("Quiz cannot be started after the schedule end time!"));
        }

        var questionPackage =
            await questionPackageRepository.GetQuestionPackageByIdAsync(quiz.QuestionPackageId, cancellationToken);

        if (questionPackage is null)
        {
            return Result<CreateQuizSessionResponse>.Failure(Error.Validation("Question package not found!"));
        }

        var newQuizSession =
            quizSessionFactory.CreateQuizSession(request.UserId, request.QuizId, questionPackage.Questions.Count);

        await quizSessionRepository.InsertQuizSessionAsync(newQuizSession, cancellationToken);

        var quizSessionDto = new QuizSessionDto()
        {
            Id = newQuizSession.Id,
            QuizId = newQuizSession.QuizId,
            UserId = newQuizSession.UserId,
            StartedAt = newQuizSession.StartedAt,
            FinalizedAt = newQuizSession.FinalizedAt,
            Status = newQuizSession.Status,
            CorrectAnswers = newQuizSession.CorrectAnswers,
            TotalQuestions = newQuizSession.TotalQuestions
        };

        return Result<CreateQuizSessionResponse>.Success(new CreateQuizSessionResponse(quizSessionDto));
    }
}