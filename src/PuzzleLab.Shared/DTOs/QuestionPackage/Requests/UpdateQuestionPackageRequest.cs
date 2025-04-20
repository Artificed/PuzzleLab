namespace PuzzleLab.Shared.DTOs.QuestionPackage.Requests;

public class UpdateQuestionPackageRequest
{
    public string Id { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
}