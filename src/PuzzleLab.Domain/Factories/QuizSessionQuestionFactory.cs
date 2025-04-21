using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class QuizSessionQuestionFactory
{
    public QuizSessionQuestion CreateQuizSessionQuestion(Guid quizSessionId, Guid quizQuestionId, int questionNumber)
    {
        return new QuizSessionQuestion(
            quizSessionId,
            quizQuestionId,
            questionNumber
        );
    }
}