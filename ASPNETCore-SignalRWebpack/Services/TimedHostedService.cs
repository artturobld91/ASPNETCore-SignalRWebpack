using ASPNETCore_SignalRWebpack.Hubs;
using ASPNETCore_SignalRWebpack.Interface;
using Microsoft.AspNetCore.SignalR;

namespace ASPNETCore_SignalRWebpack.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IHubContext<ChatHub, IChatHub> _chathub;
        private Timer _timer = null!;

        public TimedHostedService(ILogger<TimedHostedService> logger,
                                  IHubContext<ChatHub, IChatHub> chathub)
        {
            _logger = logger;
            _chathub = chathub;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            await BroadcastMsg(count);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        public async Task BroadcastMsg(int count)
        {
            if (_chathub.Clients != null)
                await _chathub.Clients.All.BroadcastMessage("ASP.NET Server", $"Hello ({count})");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
