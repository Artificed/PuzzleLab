using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class AnswerFactory
{
    public Answer CreateAnswer(Guid questionId, string text, bool isCorrect)
    {
        return new Answer(
            Guid.NewGuid(),
            questionId,
            text,
            isCorrect
        );
    }
}