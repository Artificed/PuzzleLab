namespace PuzzleLab.Shared.DTOs.QuizSession;

public class UserQuizStatisticsDto
{
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public List<QuizResultDto> QuizResults { get; set; } = new();
}