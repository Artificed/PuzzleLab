using MediatR;
using PuzzleLab.Application.Common.Models;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.User;
using PuzzleLab.Shared.DTOs.User.Responses;

namespace PuzzleLab.Application.Features.User.Commands;

public class UpdateUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse>>
{
    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            return Result<UpdateUserResponse>.Failure(Error.NotFound("User not found!"));
        }

        user.UpdateEmail(request.Email);
        user.UpdateUsername(request.Username);
        user.UpdatePasswordHash(BCrypt.Net.BCrypt.HashPassword(request.Password));

        await userRepository.UpdateUserAsync(user, cancellationToken);

        var userDto = new UserDto(
            user.Id,
            user.Username,
            user.Email,
            user.CreatedAt,
            user.LastLoginAt
        );

        return Result<UpdateUserResponse>.Success(new UpdateUserResponse(userDto));
    }
}