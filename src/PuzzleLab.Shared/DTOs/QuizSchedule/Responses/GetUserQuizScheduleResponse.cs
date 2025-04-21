using PuzzleLab.Shared.DTOs.QuizSession;

namespace PuzzleLab.Shared.DTOs.QuizSchedule.Responses;

public record GetUserQuizScheduleResponse(List<QuizScheduleDto> QuizSessionDtos);