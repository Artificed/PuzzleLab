namespace PuzzleLab.Web.Services.Time;

public interface IServerTimeService : IAsyncDisposable
{
    DateTimeOffset? CurrentServerTimeUtc { get; }
    event Action? TimeUpdated;
    Task InitializeAsync();
}