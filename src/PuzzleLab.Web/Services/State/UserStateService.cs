using PuzzleLab.Shared.DTOs.Responses;
using PuzzleLab.Web.Services.Api.Interfaces;

namespace PuzzleLab.Web.Services.State;

public class UserStateService
{
    private readonly IAuthService _authService;
    private readonly ITokenProvider _tokenProvider;

    public GetCurrentUserResponse? CurrentUser { get; private set; }
    public bool IsInitialized { get; private set; } = false;
    public bool IsLoading { get; private set; } = false;

    public event Action? OnChange;

    public UserStateService(IAuthService authService, ITokenProvider tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
    }

    public async Task InitializeAsync()
    {
        if (IsInitialized || IsLoading) return;

        IsLoading = true;
        NotifyStateChanged();

        try
        {
            string? token = null;
            try
            {
                token = await _tokenProvider.GetTokenAsync();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(
                    $"UserStateService.InitializeAsync: Error checking token (likely prerendering): {e.Message}");
            }


            if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine("UserStateService.InitializeAsync: Token found, attempting to get current user.");
                CurrentUser = await _authService.GetCurrentUserAsync();
                Console.WriteLine(
                    $"UserStateService.InitializeAsync: GetCurrentUserAsync result: {(CurrentUser == null ? "null" : CurrentUser.Email)}");
            }
            else
            {
                Console.WriteLine("UserStateService.InitializeAsync: No token found.");
                CurrentUser = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"UserStateService.InitializeAsync: Error getting current user: {ex.Message}");
            CurrentUser = null;
        }
        finally
        {
            IsLoading = false;
            IsInitialized = true;
            NotifyStateChanged();
        }
    }

    public async Task UpdateUserAfterLoginAsync()
    {
        IsInitialized = false;
        await InitializeAsync();
    }

    public void ClearUser()
    {
        CurrentUser = null;
        IsInitialized = true;
        IsLoading = false;
        NotifyStateChanged();
    }


    private void NotifyStateChanged() => OnChange?.Invoke();
}