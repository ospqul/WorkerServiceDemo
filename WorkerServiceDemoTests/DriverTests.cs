using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WorkerServiceDemo;
using Xunit;

namespace WorkerServiceDemoTests
{
    public class DriverTests
    {
        public ILogger<Driver> logger;

        public DriverTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            logger = factory.CreateLogger<Driver>();
        }

        [Theory]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 3)]
        [InlineData(4, 4)]
        [InlineData(4, 5)]
        public void Driver_ShouldNotTakePassengersMoreThanCapacity(int capacity, int passengers)
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"Capacity", capacity.ToString()},
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Setup

            var driver = new Driver(logger, config);

            var actual = driver.Drive(passengers);

            var expected = capacity >= passengers;

            Assert.Equal(expected, actual);
        }
    }
}
