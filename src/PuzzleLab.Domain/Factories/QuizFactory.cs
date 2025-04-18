using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class QuizFactory
{
    public Quiz CreateQuiz(Guid questionPackageId, Guid scheduleId)
    {
        return new Quiz(
            Guid.NewGuid(),
            questionPackageId,
            scheduleId
        );
    }
}