using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.Auth.Responses;

namespace PuzzleLab.Application.Features.Auth.Queries;

public record GetCurrentUserQuery(Guid UserId) : IRequest<Result<GetCurrentUserResponse>>;