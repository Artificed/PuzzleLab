using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class QuizUserFactory
{
    public QuizUser CreateQuizUser(Guid userId, Guid quizId)
    {
        return new QuizUser(
            Guid.NewGuid(),
            userId,
            quizId
        );
    }
}