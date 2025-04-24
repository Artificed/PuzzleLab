using Microsoft.AspNetCore.SignalR.Client;

namespace PuzzleLab.Web.Services.Time
{
    public class ServerTimeService : IServerTimeService
    {
        private readonly IConfiguration _configuration;
        private HubConnection? _hubConnection;
        private DateTimeOffset? _currentServerTimeUtc;
        private readonly ILogger<ServerTimeService> _logger;

        public event Action? TimeUpdated;

        public DateTimeOffset? CurrentServerTimeUtc => _currentServerTimeUtc;

        public ServerTimeService(IConfiguration configuration, ILogger<ServerTimeService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            if (_hubConnection != null)
            {
                return;
            }

            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                _logger.LogError("'ApiSettings:BaseUrl' is not configured.");
                return;
            }

            var hubUrl = $"{apiBaseUrl.TrimEnd('/')}/hubs/time";
            _logger.LogInformation("Initializing ServerTimeService. Connecting to SignalR Hub at: {HubUrl}", hubUrl);

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<DateTimeOffset>("ReceiveServerTime", (serverTime) =>
            {
                _currentServerTimeUtc = serverTime;
                TimeUpdated?.Invoke();
            });

            _hubConnection.Closed += async (error) =>
            {
                _logger.LogWarning(error, "SignalR connection closed. Will attempt to reconnect automatically.");
                await Task.Delay(new Random().Next(0, 5) * 1000);
            };

            try
            {
                await _hubConnection.StartAsync();
                _logger.LogInformation("SignalR Connection for ServerTimeService started successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting SignalR connection for ServerTimeService to {HubUrl}", hubUrl);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                _logger.LogInformation("Disposing ServerTimeService SignalR connection.");
                try
                {
                    await _hubConnection.StopAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error stopping SignalR connection during disposal.");
                }
                finally
                {
                    await _hubConnection.DisposeAsync();
                    _hubConnection = null;
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}