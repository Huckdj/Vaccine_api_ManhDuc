using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vaccine_api_ManhDuc.Controllers.AuthUserAdmin;
using Vaccine_api_ManhDuc.Data;
using Microsoft.Extensions.Configuration;
using NodaTime;
using NodaTime.TimeZones;

namespace Vaccine_api_ManhDuc.Services
{
    public class ScheduledTaskService : IHostedService, IDisposable
    {
        private readonly ILogger<ScheduledTaskService> _logger;
        private Timer _timer;
        private readonly IConfiguration _configuration;

        public ScheduledTaskService(ILogger<ScheduledTaskService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var vietnamTimeZone = DateTimeZoneProviders.Tzdb["Asia/Ho_Chi_Minh"];
            var clock = SystemClock.Instance;
            var now = clock.GetCurrentInstant().InZone(vietnamTimeZone).ToDateTimeUnspecified();

            var firstRun = now.Date.AddHours(7);

            if (now > firstRun)
            {
                firstRun = firstRun.AddDays(1);
            }

            var initialDelay = firstRun - now;
            var interval = TimeSpan.FromDays(1);

            _timer = new Timer(ExecuteTask, null, initialDelay, interval);
            return Task.CompletedTask;
        }

        private void ExecuteTask(object state)
        {
            try
            {
                var controller = new ScanBookingController(_configuration);
                

                controller.ScanDaily().Wait();

                _logger.LogInformation("Task executed successfully at {time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while executing task: {message}", ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
