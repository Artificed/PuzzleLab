using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Commands;

public record DeleteUserCommand(Guid Id) : IRequest<Result<DeleteUserResponse>>;