using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSession;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Commands;

public class CreateOrGetQuizSessionCommandHandler(
    IQuizRepository quizRepository,
    IUserRepository userRepository,
    IQuizUserRepository quizUserRepository,
    IScheduleRepository scheduleRepository,
    QuizSessionFactory quizSessionFactory,
    IQuizSessionRepository quizSessionRepository,
    QuizSessionQuestionFactory quizSessionQuestionFactory,
    IQuizSessionQuestionRepository quizSessionQuestionRepository,
    IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<CreateOrGetQuizSessionCommand, Result<CreateOrGetQuizSessionResponse>>
{
    public async Task<Result<CreateOrGetQuizSessionResponse>> Handle(CreateOrGetQuizSessionCommand request,
        CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.GetQuizByIdAsync(request.QuizId, cancellationToken);
        if (quiz is null)
            return Result<CreateOrGetQuizSessionResponse>.Failure(Error.NotFound("Quiz not found!"));

        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result<CreateOrGetQuizSessionResponse>.Failure(Error.NotFound("User not found!"));

        var quizUser = await quizUserRepository.GetByUserIdAndQuizIdAsync(user.Id, quiz.Id, cancellationToken);
        if (quizUser is null)
            return Result<CreateOrGetQuizSessionResponse>.Failure(
                Error.NotFound("User is not registered for this quiz!"));

        var schedule = await scheduleRepository.GetScheduleByIdAsync(quiz.ScheduleId, cancellationToken);
        if (schedule is null)
            return Result<CreateOrGetQuizSessionResponse>.Failure(Error.NotFound("Schedule not found!"));

        if (schedule.StartDateTime > DateTime.Now)
            return Result<CreateOrGetQuizSessionResponse>.Failure(
                Error.Validation("Quiz cannot be started before the schedule start time!"));

        if (schedule.EndDateTime < DateTime.Now)
            return Result<CreateOrGetQuizSessionResponse>.Failure(
                Error.Validation("Quiz cannot be started after the schedule end time!"));

        var existingSession =
            await quizSessionRepository.GetExistingQuizSessionAsync(request.QuizId, request.UserId, cancellationToken);
        if (existingSession is not null && existingSession.FinalizedAt is null && DateTime.Now < schedule.EndDateTime)
        {
            var dto = MapToDto(existingSession);
            return Result<CreateOrGetQuizSessionResponse>.Success(new CreateOrGetQuizSessionResponse(dto));
        }

        var questionPackage =
            await questionPackageRepository.GetQuestionPackageByIdAsync(quiz.QuestionPackageId, cancellationToken);
        if (questionPackage is null)
            return Result<CreateOrGetQuizSessionResponse>.Failure(Error.Validation("Question package not found!"));

        var newSession =
            quizSessionFactory.CreateQuizSession(request.UserId, request.QuizId, questionPackage.Questions.Count);
        await quizSessionRepository.InsertQuizSessionAsync(newSession, cancellationToken);

        await InsertShuffledQuestions(newSession.Id, questionPackage.Questions.ToList(), cancellationToken);

        var newDto = MapToDto(newSession);
        return Result<CreateOrGetQuizSessionResponse>.Success(new CreateOrGetQuizSessionResponse(newDto));
    }

    private static QuizSessionDto MapToDto(Domain.Entities.QuizSession session) => new()
    {
        Id = session.Id,
        QuizId = session.QuizId,
        UserId = session.UserId,
        StartedAt = session.StartedAt,
        FinalizedAt = session.FinalizedAt,
        Status = session.Status,
        CorrectAnswers = session.CorrectAnswers,
        TotalQuestions = session.TotalQuestions
    };

    private async Task InsertShuffledQuestions(Guid sessionId, List<Domain.Entities.Question> questions,
        CancellationToken cancellationToken)
    {
        var rng = new Random();
        var shuffled = questions.OrderBy(_ => rng.Next()).ToList();

        for (int i = 0; i < shuffled.Count; i++)
        {
            var quizSessionQuestion =
                quizSessionQuestionFactory.CreateQuizSessionQuestion(sessionId, shuffled[i].Id, i);
            await quizSessionQuestionRepository.InsertQuizSessionQuestionAsync(quizSessionQuestion, cancellationToken);
        }
    }
}