using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.QuizSession;
using PuzzleLab.Shared.DTOs.QuizSession.Responses;

namespace PuzzleLab.Application.Features.QuizSession.Queries;

public class GetQuizResultQueryHandler(
    IQuizSessionRepository quizSessionRepository,
    IQuizRepository quizRepository,
    IUserRepository userRepository
) : IRequestHandler<GetQuizResultQuery, Result<GetQuizResultResponse>>
{
    public async Task<Result<GetQuizResultResponse>> Handle(GetQuizResultQuery request,
        CancellationToken cancellationToken)
    {
        var quiz = await quizRepository.GetQuizByIdAsync(request.QuizId, cancellationToken);

        if (quiz is null)
        {
            return Result<GetQuizResultResponse>.Failure(Error.NotFound("Quiz not found!"));
        }

        var quizSessions = await quizSessionRepository.GetQuizSessionsByQuizAsync(quiz.Id, cancellationToken);

        var quizResultDtos = new List<QuizResultDto>();

        foreach (var qs in quizSessions.Where(q => q.FinalizedAt.HasValue))
        {
            if (quiz == null || qs.FinalizedAt == null)
                continue;

            var scorePercentage = qs.TotalQuestions > 0
                ? (double)qs.CorrectAnswers / qs.TotalQuestions * 100
                : 0;

            var user = await userRepository.GetUserByIdAsync(qs.UserId, cancellationToken);

            if (user == null)
            {
                return Result<GetQuizResultResponse>.Failure(Error.NotFound("User not found!"));
            }

            quizResultDtos.Add(new QuizResultDto()
            {
                UserId = qs.UserId,
                Username = user.Username,
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

        return Result<GetQuizResultResponse>.Success(new GetQuizResultResponse(quizResultDtos));
    }
}