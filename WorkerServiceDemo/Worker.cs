using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerServiceDemo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDriver _driver;

        public Worker(ILogger<Worker> logger, IDriver driver)
        {
            _logger = logger;
            _driver = driver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                if (_driver.Drive(i))
                {
                    i++;
                }
                else
                {
                    break;
                }                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
