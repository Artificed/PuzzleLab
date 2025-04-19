using PuzzleLab.Application.Common.Interfaces;

namespace PuzzleLab.Infrastructure.Security;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PuzzleLab.Domain.Entities;

public class JwtGenerator : IJwtGenerator
{
    private readonly SymmetricSecurityKey _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly double _expiryMinutes;

    public JwtGenerator(IConfiguration configuration)
    {
        var secretKey = configuration["JwtSettings:Key"]
                        ?? throw new InvalidOperationException("JWT Key is not configured in JwtSettings:Key");

        var keyBytes = Encoding.ASCII.GetBytes(secretKey);
        const int minKeySizeInBytes = 128 / 8;
        if (keyBytes.Length < minKeySizeInBytes)
        {
            throw new InvalidOperationException(
                $"JWT Key must be at least {minKeySizeInBytes * 8} bits ({minKeySizeInBytes} bytes) long for HS256. Current length: {keyBytes.Length * 8} bits.");
        }

        _key = new SymmetricSecurityKey(keyBytes);

        _issuer = configuration["JwtSettings:Issuer"]
                  ?? throw new InvalidOperationException("JWT Issuer is not configured in JwtSettings:Issuer");
        _audience = configuration["JwtSettings:Audience"]
                    ?? throw new InvalidOperationException("JWT Audience is not configured in JwtSettings:Audience");

        if (!double.TryParse(configuration["JwtSettings:ExpiryMinutes"], out _expiryMinutes))
        {
            _expiryMinutes = 60;
        }
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expiryMinutes),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}