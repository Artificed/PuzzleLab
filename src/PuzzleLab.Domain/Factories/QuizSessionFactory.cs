using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class QuizSessionFactory
{
    public QuizSession CreateQuizSession(Guid userId, Guid quizId, int totalQuestions)
    {
        return new QuizSession(
            Guid.NewGuid(),
            userId,
            quizId,
            totalQuestions
        );
    }
}