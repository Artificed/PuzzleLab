namespace PuzzleLab.Web.Services.Api.Interfaces;

public interface ITokenProvider
{
    Task<string?> GetTokenAsync();
    Task SetTokenAsync(string token);
}