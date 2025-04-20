namespace PuzzleLab.Shared.DTOs.QuizSchedule;

public class QuizScheduleDto
{
    public Guid QuizId { get; set; }
    public Guid QuizPackageId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public int ParticipantCount { get; set; }
    public int QuestionCount { get; set; }
}