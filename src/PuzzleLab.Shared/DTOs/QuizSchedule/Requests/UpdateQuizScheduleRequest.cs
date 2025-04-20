namespace PuzzleLab.Shared.DTOs.QuizSchedule.Requests;

public class UpdateQuizScheduleRequest
{
    public Guid QuizId { get; set; }
    public Guid QuestionPackageId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}