using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Commands;

public class DeleteQuizScheduleCommandHandler(IScheduleRepository scheduleRepository, IQuizRepository quizRepository)
    : IRequestHandler<DeleteQuizScheduleCommand, Result<DeleteQuizScheduleResponse>>
{
    public async Task<Result<DeleteQuizScheduleResponse>> Handle(DeleteQuizScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.GetQuizByIdAsync(request.QuizId, cancellationToken);
        if (quiz is null)
        {
            return Result<DeleteQuizScheduleResponse>.Failure(Error.NotFound("Quiz not found!"));
        }

        var schedule = await scheduleRepository.GetScheduleByIdAsync(quiz.ScheduleId, cancellationToken);
        if (schedule is null)
        {
            return Result<DeleteQuizScheduleResponse>.Failure(Error.NotFound("Schedule not found!"));
        }

        await quizRepository.DeleteQuizAsync(quiz, cancellationToken);
        await scheduleRepository.DeleteScheduleAsync(schedule, cancellationToken);

        return Result<DeleteQuizScheduleResponse>.Success(new DeleteQuizScheduleResponse(quiz.Id));
    }
}