using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using PuzzleLab.Application.Common;
using PuzzleLab.Application.Common.Interfaces;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Application.Features.Auth.Commands;

public class LoginCommandHandler(IUserRepository userRepository, IJwtGenerator jwtGenerator)
    : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return Result<string>.Failure(Error.Validation("Email and password are required!"));
        }

        var user = await userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure(Error.NotFound($"User with email {request.Email} not found!"));
        }

        Console.WriteLine(request);

        var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            return Result<string>.Failure(Error.Unauthorized("Invalid password!"));
        }

        var tokenString = jwtGenerator.GenerateToken(user);

        return Result<string>.Success(tokenString);
    }
}