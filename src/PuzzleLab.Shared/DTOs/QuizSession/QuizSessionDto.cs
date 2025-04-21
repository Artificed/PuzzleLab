namespace PuzzleLab.Shared.DTOs.QuizSession;

public class QuizSessionDto
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? FinalizedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }
}