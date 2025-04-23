using PuzzleLab.Domain.Common;
using PuzzleLab.Domain.Events;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Application.Features.QuizSession.Events;

public class QuizFinalizedEventHandler(
    IQuizSessionRepository quizSessionRepository,
    IQuizAnswerRepository quizAnswerRepository)
    : IDomainEventHandler<QuizFinalizedEvent>
{
    public async Task HandleAsync(QuizFinalizedEvent @event, CancellationToken cancellationToken)
    {
        var sessionId = @event.SessionId;
        var session = await quizSessionRepository.GetQuizSessionByIdAsync(sessionId, cancellationToken);

        if (session is null)
        {
            throw new Exception("Quiz session not found!");
        }

        var quizAnswers = await quizAnswerRepository.GetBySessionIdAsync(sessionId, cancellationToken);
        var correctCount = quizAnswers.Count(x => x.IsCorrect);
        session.UpdateCorrectAnswers(correctCount);

        await quizSessionRepository.UpdateQuizSessionAsync(session, cancellationToken);
    }
}