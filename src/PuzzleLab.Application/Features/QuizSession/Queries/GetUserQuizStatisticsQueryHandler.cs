using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSession;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Queries;

public class GetUserQuizStatisticsQueryHandler(
    IUserRepository userRepository,
    IQuizSessionRepository quizSessionRepository,
    IQuizRepository quizRepository
) : IRequestHandler<GetUserQuizStatisticsQuery, Result<GetUserQuizStatisticsResponse>>
{
    public async Task<Result<GetUserQuizStatisticsResponse>> Handle(GetUserQuizStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<GetUserQuizStatisticsResponse>.Failure(Error.NotFound("User not found!"));
        }

        var quizSessions = await quizSessionRepository.GetQuizSessionsByUserIdAsync(user.Id, cancellationToken);

        var quizResultDtos = new List<QuizResultDto>();

        foreach (var qs in quizSessions.Where(q => q.FinalizedAt.HasValue))
        {
            var quiz = await quizRepository.GetQuizByIdAsync(qs.QuizId, cancellationToken);

            if (quiz == null || qs.FinalizedAt == null)
                continue;

            var scorePercentage = qs.TotalQuestions > 0
                ? (double)qs.CorrectAnswers / qs.TotalQuestions * 100
                : 0;

            quizResultDtos.Add(new QuizResultDto
            {
                QuizId = qs.QuizId,
                QuizName = quiz.QuestionPackage.Name,
                ScheduleStartDate = qs.StartedAt,
                QuizCompletedDate = qs.FinalizedAt.Value,
                TotalQuestions = qs.TotalQuestions,
                CorrectAnswers = qs.CorrectAnswers,
                ScorePercentage = (decimal)scorePercentage,
                CompletionTime = (qs.FinalizedAt.Value - qs.StartedAt)
            });
        }

        var responseData = new UserQuizStatisticsDto
        {
            UserId = user.Id.ToString(),
            Username = user.Username,
            QuizResults = quizResultDtos
        };

        return Result<GetUserQuizStatisticsResponse>.Success(new GetUserQuizStatisticsResponse(responseData));
    }
}