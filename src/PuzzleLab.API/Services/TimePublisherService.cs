using Microsoft.AspNetCore.SignalR;
using PuzzleLab.API.Hubs;

namespace PuzzleLab.API.Services
{
    public class TimePublisherService : IHostedService, IDisposable
    {
        private readonly IHubContext<TimeHub> _hubContext;
        private Timer? _timer;
        private readonly ILogger<TimePublisherService> _logger;

        public TimePublisherService(IHubContext<TimeHub> hubContext, ILogger<TimePublisherService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Time Publisher Service is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var currentTime = DateTimeOffset.UtcNow;
            _logger.LogDebug($"Publishing time: {currentTime}");

            try
            {
                await _hubContext.Clients.All.SendAsync("ReceiveServerTime", currentTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing time via SignalR.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Time Publisher Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}