using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WorkerServiceDemo
{
    public class Driver : IDriver
    {
        private readonly ILogger<Driver> _logger;
        private readonly IConfiguration _config;
        private int capacity;

        public Driver(ILogger<Driver> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            capacity = _config.GetValue<int>("Capacity");
        }

        public bool Drive(int passengers)
        {
            if (passengers > capacity)
            {
                _logger.LogError("Too many passengers, we can only take {Capacity}!", capacity);
                return false;
            }
            else
            {
                _logger.LogInformation("Driver is driving with {Passengers}...", passengers);
                return true;
            }
        }
    }
}
