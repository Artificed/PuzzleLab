using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSchedule;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Commands;

public class UpdateQuizScheduleCommandHandler(
    IScheduleRepository scheduleRepository,
    IQuestionPackageRepository questionPackageRepository,
    IQuizRepository quizRepository)
    : IRequestHandler<UpdateQuizScheduleCommand, Result<UpdateQuizScheduleResponse>>
{
    public async Task<Result<UpdateQuizScheduleResponse>> Handle(UpdateQuizScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var questionPackage = await questionPackageRepository.GetQuestionPackageByIdAsync(request.QuestionPackageId,
            cancellationToken);

        if (questionPackage is null)
        {
            throw new Exception("Question package not found!");
        }

        var quiz = await quizRepository.GetQuizByIdAsync(request.QuizId, cancellationToken);
        if (quiz is null)
        {
            return Result<UpdateQuizScheduleResponse>.Failure(Error.NotFound("Quiz not found!"));
        }

        var schedule = await scheduleRepository.GetScheduleByIdAsync(quiz.ScheduleId, cancellationToken);
        if (schedule is null)
        {
            return Result<UpdateQuizScheduleResponse>.Failure(Error.NotFound("Schedule not found!"));
        }

        quiz.UpdateQuestionPackageId(request.QuestionPackageId);
        await quizRepository.UpdateQuizAsync(quiz, cancellationToken);

        schedule.UpdateTimeWindow(request.StartTime, request.EndTime);
        await scheduleRepository.UpdateScheduleAsync(schedule, cancellationToken);

        var quizScheduleDto = new QuizScheduleDto()
        {
            QuizId = quiz.Id,
            QuizPackageId = questionPackage.Id,
            Title = questionPackage.Name,
            Description = questionPackage.Description,
            StartTime = schedule.StartDateTime,
            EndTime = schedule.EndDateTime,
            QuestionCount = questionPackage.Questions.Count,
            ParticipantCount = quiz.QuizUsers.Count
        };

        return Result<UpdateQuizScheduleResponse>.Success(new UpdateQuizScheduleResponse(quizScheduleDto));
    }
}