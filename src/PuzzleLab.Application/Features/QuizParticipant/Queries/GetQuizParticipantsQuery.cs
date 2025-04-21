using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.QuizUser.Responses;

namespace PuzzleLab.Application.Features.QuizParticipant.Queries;

public record GetQuizParticipantsQuery(Guid QuizId) : IRequest<Result<GetQuizParticipantsResponse>>;