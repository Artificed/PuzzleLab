using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Commands;

public record UpdateUserCommand(Guid Id, string Username, string Email, string Password) : IRequest<Result<UpdateUserResponse>>;