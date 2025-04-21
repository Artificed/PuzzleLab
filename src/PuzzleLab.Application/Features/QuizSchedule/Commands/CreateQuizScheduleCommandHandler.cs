using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSchedule;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Commands;

public class CreateQuizScheduleCommandHandler(
    IScheduleRepository scheduleRepository,
    IQuizRepository quizRepository,
    IQuestionPackageRepository questionPackageRepository,
    QuizFactory quizFactory,
    ScheduleFactory scheduleFactory)
    : IRequestHandler<CreateQuizScheduleCommand, Result<CreateQuizScheduleResponse>>
{
    public async Task<Result<CreateQuizScheduleResponse>> Handle(CreateQuizScheduleCommand request,
        CancellationToken cancellationToken)
    {
        var questionPackage = await questionPackageRepository.GetQuestionPackageByIdAsync(request.QuestionPackageId,
            cancellationToken);

        if (questionPackage is null)
        {
            return Result<CreateQuizScheduleResponse>.Failure(Error.NotFound("Question package not found!"));
        }

        var newSchedule = scheduleFactory.CreateSchedule(request.StartTime, request.EndTime);
        await scheduleRepository.InsertScheduleAsync(newSchedule, cancellationToken);

        var newQuiz = quizFactory.CreateQuiz(request.QuestionPackageId, newSchedule.Id);
        await quizRepository.InsertQuizAsync(newQuiz, cancellationToken);


        var quizScheduleDto = new QuizScheduleDto()
        {
            QuizId = newQuiz.Id,
            QuizPackageId = newQuiz.QuestionPackageId,
            Title = questionPackage.Name,
            Description = questionPackage.Description,
            StartTime = newSchedule.StartDateTime,
            EndTime = newSchedule.EndDateTime,
            QuestionCount = 0,
            ParticipantCount = 0
        };

        return Result<CreateQuizScheduleResponse>.Success(new CreateQuizScheduleResponse(quizScheduleDto));
    }
}