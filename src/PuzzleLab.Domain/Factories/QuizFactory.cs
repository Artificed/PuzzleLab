using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class QuizFactory
{
    public Quiz CreateQuiz(Guid questionPackageId, Guid? scheduleId = null)
    {
        return new Quiz(
            Guid.NewGuid(),
            questionPackageId,
            scheduleId
        );
    }
}