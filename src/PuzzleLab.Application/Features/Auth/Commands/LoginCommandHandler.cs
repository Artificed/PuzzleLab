using MediatR;
using PuzzleLab.Application.Common;
using PuzzleLab.Application.Common.Interfaces;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Shared.DTOs.Responses;

namespace PuzzleLab.Application.Features.Auth.Commands;

public class LoginCommandHandler(IUserRepository userRepository, IJwtGenerator jwtGenerator)
    : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return Result<LoginResponse>.Failure(Error.Validation("Email and password are required!"));
        }

        var user = await userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result<LoginResponse>.Failure(Error.NotFound($"User with email {request.Email} not found!"));
        }

        Console.WriteLine(request);

        var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            return Result<LoginResponse>.Failure(Error.Unauthorized("Invalid password!"));
        }

        var tokenString = jwtGenerator.GenerateToken(user);

        return Result<LoginResponse>.Success(new LoginResponse(){Token = tokenString});
    }
}