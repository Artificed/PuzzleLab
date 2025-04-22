using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Answer;
using PuzzleLab.Shared.DTOs.QuizSession;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Queries;

public class GetCurrentQuestionQueryCommandHandler(
    IQuizRepository quizRepository,
    IUserRepository userRepository,
    IQuizUserRepository quizUserRepository,
    IScheduleRepository scheduleRepository,
    IQuestionRepository questionRepository,
    IAnswerRepository answerRepository,
    IQuizSessionRepository quizSessionRepository,
    IQuizSessionQuestionRepository quizSessionQuestionRepository
)
    : IRequestHandler<GetCurrentQuestionQuery, Result<GetCurrentQuestionResponse>>
{
    public async Task<Result<GetCurrentQuestionResponse>> Handle(GetCurrentQuestionQuery request,
        CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.GetQuizByIdAsync(request.QuizId, cancellationToken);
        if (quiz is null)
            return Result<GetCurrentQuestionResponse>.Failure(Error.NotFound("Quiz not found!"));

        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result<GetCurrentQuestionResponse>.Failure(Error.NotFound("User not found!"));

        var quizUser = await quizUserRepository.GetByUserIdAndQuizIdAsync(user.Id, quiz.Id, cancellationToken);
        if (quizUser is null)
            return Result<GetCurrentQuestionResponse>.Failure(
                Error.NotFound("User is not registered for this quiz!"));

        var schedule = await scheduleRepository.GetScheduleByIdAsync(quiz.ScheduleId, cancellationToken);
        if (schedule is null)
            return Result<GetCurrentQuestionResponse>.Failure(Error.NotFound("Schedule not found!"));

        if (schedule.StartDateTime > DateTime.Now)
            return Result<GetCurrentQuestionResponse>.Failure(
                Error.Validation("Quiz cannot be started before the schedule start time!"));

        if (schedule.EndDateTime < DateTime.Now)
            return Result<GetCurrentQuestionResponse>.Failure(
                Error.Validation("Quiz cannot be started after the schedule end time!"));

        var existingSession =
            await quizSessionRepository.GetExistingQuizSessionAsync(request.QuizId, request.UserId, cancellationToken);
        if (existingSession is null || existingSession.FinalizedAt is not null || DateTime.Now > schedule.EndDateTime)
        {
            return Result<GetCurrentQuestionResponse>.Failure(Error.Validation("Invalid quiz session!"));
        }


        // Fetching the data
        var quizSessionQuestion = await quizSessionQuestionRepository.GetQuizSessionQuestionByIdAsync(
            existingSession.Id, request.CurrentIndex, cancellationToken);

        if (quizSessionQuestion is null)
        {
            return Result<GetCurrentQuestionResponse>.Failure(Error.NotFound("Quiz session question not found!"));
        }

        var question = await questionRepository.GetQuestionByIdAsync(quizSessionQuestion.QuestionId, cancellationToken);
        if (question is null)
        {
            return Result<GetCurrentQuestionResponse>.Failure(Error.NotFound("Question not found!"));
        }

        var answers =
            await answerRepository.GetAnswersByQuestionIdAsync(question.Id, cancellationToken);
        var answerOptionDtos = answers.Select(a => new AnswerOptionDto() { Id = a.Id, Text = a.Text }).ToArray();

        var questionWithAnswer = new QuestionWithAnswerDto()
        {
            Id = question.Id,
            Text = question.Text,
            ImageData = question.ImageData,
            ImageMimeType = question.ImageMimeType,
            Answers = answerOptionDtos
        };

        return Result<GetCurrentQuestionResponse>.Success(new GetCurrentQuestionResponse(questionWithAnswer));
    }
}