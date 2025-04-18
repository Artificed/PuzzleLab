using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class QuizAnswerFactory
{
    public QuizAnswer CreateQuizAnswer(Guid quizSessionId, Guid questionId, Guid selectedAnswerId, bool isCorrect)
    {
        return new QuizAnswer(
            Guid.NewGuid(),
            quizSessionId,
            questionId,
            selectedAnswerId,
            isCorrect
        );
    }
}