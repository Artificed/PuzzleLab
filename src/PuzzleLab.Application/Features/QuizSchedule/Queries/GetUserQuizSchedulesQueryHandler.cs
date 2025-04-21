using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSchedule;
using PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

namespace PuzzleLab.Application.Features.QuizSchedule.Queries;

public class GetUserQuizSchedulesQueryHandler(
    IQuizRepository quizRepository,
    IScheduleRepository scheduleRepository,
    IQuestionPackageRepository questionPackageRepository)
    : IRequestHandler<GetUserQuizScheduleQuery, Result<GetUserQuizScheduleResponse>>
{
    public async Task<Result<GetUserQuizScheduleResponse>> Handle(GetUserQuizScheduleQuery request,
        CancellationToken cancellationToken)
    {
        var quizzes = await quizRepository.GetAllQuizzesAsync(cancellationToken);

        var userQuizzes = quizzes
            .Where(q => q.QuizUsers.Any(u => u.UserId == request.UserId))
            .ToList();

        var quizScheduleDtos = new List<QuizScheduleDto>();

        foreach (var quiz in userQuizzes)
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

        var response = new GetUserQuizScheduleResponse(quizScheduleDtos);

        return Result<GetUserQuizScheduleResponse>.Success(response);
    }
}