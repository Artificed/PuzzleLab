using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class QuestionPackageFactory
{
    public QuestionPackage CreateQuestionPackage(string name, string description)
    {
        return new QuestionPackage(
            Guid.NewGuid(),
            name,
            description
        );
    }
}