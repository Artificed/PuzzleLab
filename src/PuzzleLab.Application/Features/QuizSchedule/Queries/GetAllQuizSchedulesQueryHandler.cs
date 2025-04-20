using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSchedule;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Queries;

public class GetAllQuizSchedulesQueryHandler(
    IQuizRepository quizRepository,
    IScheduleRepository scheduleRepository,
    IQuestionPackageRepository questionPackageRepository) :
    IRequestHandler<GetAllQuizSchedulesQuery, Result<GetAllQuizSchedulesResponse>>
{
    public async Task<Result<GetAllQuizSchedulesResponse>> Handle(GetAllQuizSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        var quizzes = await quizRepository.GetAllQuizzesAsync(cancellationToken);

        var quizScheduleDtos = new List<QuizScheduleDto>();

        foreach (var quiz in quizzes)
        {
            var questionPackage =
                await questionPackageRepository.GetQuestionPackageByIdAsync(quiz.QuestionPackageId, cancellationToken);
            var schedule = await scheduleRepository.GetScheduleByIdAsync(quiz.ScheduleId, cancellationToken);

            if (questionPackage is null)
                throw new Exception($"QuestionPackage with ID {quiz.QuestionPackageId} not found.");

            if (schedule is null)
                throw new Exception($"Schedule with ID {quiz.ScheduleId} not found.");

            quizScheduleDtos.Add(new QuizScheduleDto
            {
                QuizId = quiz.Id,
                QuizPackageId = questionPackage.Id,
                Title = questionPackage.Name,
                Description = questionPackage.Description,
                StartTime = schedule.StartDateTime,
                EndTime = schedule.EndDateTime,
                ParticipantCount = quiz.QuizUsers.Count,
                QuestionCount = questionPackage.Questions.Count
            });
        }

        var response = new GetAllQuizSchedulesResponse(quizScheduleDtos.ToList());

        return Result<GetAllQuizSchedulesResponse>.Success(response);
    }
}