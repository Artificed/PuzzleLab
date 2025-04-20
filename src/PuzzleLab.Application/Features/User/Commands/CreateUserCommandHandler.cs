using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Factories;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.User;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Commands;

public class CreateUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
{
    private readonly UserFactory _userFactory = new();

    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = _userFactory.CreateUser(request.Username, request.Email, request.Password, "User");

        await userRepository.InsertUserAsync(newUser, cancellationToken);

        var userDto = new UserDto(
            newUser.Id,
            newUser.Username,
            newUser.Email,
            newUser.CreatedAt,
            newUser.LastLoginAt
        );

        return Result<CreateUserResponse>.Success(new CreateUserResponse(userDto));
    }
}