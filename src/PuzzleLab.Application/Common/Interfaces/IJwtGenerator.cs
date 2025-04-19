using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Application.Common.Interfaces;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}