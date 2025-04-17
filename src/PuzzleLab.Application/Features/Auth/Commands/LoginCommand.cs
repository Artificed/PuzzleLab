using MediatR;
using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Application.Features.Auth.Commands;

public record LoginCommand(string Username, string Password) : IRequest<User>;