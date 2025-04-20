namespace PuzzleLab.Shared.DTOs.QuestionPackage.Requests;

public class CreateQuestionPackageRequest
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
}