using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Queries;

public record GetAvailableUsersForQuizQuery(Guid QuizId) : IRequest<Result<GetAvailableUsersForQuizResponse>>;