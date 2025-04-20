namespace PuzzleLab.Shared.DTOs.Answer.Requests;

public class SaveAnswersRequest
{
    public string QuestionId { get; set; }
    public List<CreateAnswerDto> AnswerData { get; set; }
}