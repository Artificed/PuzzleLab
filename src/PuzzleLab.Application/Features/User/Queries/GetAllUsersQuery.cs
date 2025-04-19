using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Queries;

public record GetAllUsersQuery() : IRequest<Result<GetAllUsersResponse>>;