using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vaccine_api_ManhDuc.Controllers.AuthUserAdmin;
using Vaccine_api_ManhDuc.Data;
using Microsoft.Extensions.Configuration;

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
            // Đặt lịch để thực hiện công việc mỗi ngày lúc 7h sáng (theo giờ Việt Nam)
            var now = DateTime.Now;
            var firstRun = DateTime.Today.AddHours(7);

            if (now > firstRun)
            {
                firstRun = firstRun.AddDays(1);  // Nếu đã qua 7h sáng hôm nay, thực hiện vào 7h sáng ngày mai
            }

            var initialDelay = firstRun - now;
            var interval = TimeSpan.FromDays(1); // Mỗi ngày thực hiện lại

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
