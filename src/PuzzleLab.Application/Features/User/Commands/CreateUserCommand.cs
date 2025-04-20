using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Commands;

public record CreateUserCommand(string Username, string Email, string Password) : IRequest<Result<CreateUserResponse>>;