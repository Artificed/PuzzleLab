using MediatR;
using PuzzleLab.Application.Common;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Application.Features.Auth.Commands;

public class LoginCommandHandler(IUserRepository userRepository) : IRequestHandler<LoginCommand, Result<User>>
{
    public async Task<Result<User>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return Result<User>.Failure(Error.Validation("Email and password are required!"));
        }

        if (request.Password != request.ConfirmPassword)
        {
            return Result<User>.Failure(Error.Validation("Passwords do not match!"));
        }

        var user = await userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result<User>.Failure(Error.NotFound($"User with email {request.Email} not found!"));
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            return Result<User>.Failure(Error.Unauthorized("Invalid password!"));
        }

        return Result<User>.Success(user);
    }
}