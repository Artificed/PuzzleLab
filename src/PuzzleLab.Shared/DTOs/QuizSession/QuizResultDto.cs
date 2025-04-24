namespace PuzzleLab.Shared.DTOs.QuizSession;

public class QuizResultDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public Guid QuizId { get; set; }
    public string QuizName { get; set; } = string.Empty;
    public DateTime ScheduleStartDate { get; set; }
    public DateTime QuizCompletedDate { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }
    public decimal ScorePercentage { get; set; }
    public TimeSpan CompletionTime { get; set; }
}