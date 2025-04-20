namespace PuzzleLab.Shared.DTOs.QuizSchedule.Requests;

public class CreateQuizScheduleRequest
{
    public Guid QuestionPackageId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}