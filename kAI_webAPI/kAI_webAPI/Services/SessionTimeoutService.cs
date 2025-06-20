using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using kAI_webAPI.Utils;

namespace kAI_webAPI.Services
{
    public class SessionTimeoutService : BackgroundService
    {
        private readonly TimeSpan _timeout = TimeSpan.FromMinutes(20); // Timeout 20 phút
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1); // Kiểm tra mỗi phút

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var expiredSessions = Logger.GetExpiredSessions(_timeout);
                foreach (var sessionId in expiredSessions)
                {
                    Logger.EndSession(sessionId);
                }
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}