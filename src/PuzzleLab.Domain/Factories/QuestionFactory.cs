using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Factories;

public class QuestionFactory
{
    public Question CreateQuestion(Guid questionPackageId, string text)
    {
        return new Question(
            Guid.NewGuid(),
            questionPackageId,
            text
        );
    }

    public Question CreateQuestionWithImage(Guid questionPackageId, string text, byte[] imageData, string mimeType)
    {
        var question = new Question(
            Guid.NewGuid(),
            questionPackageId,
            text
        );
        question.UpdateImage(imageData, mimeType);
        return question;
    }
}