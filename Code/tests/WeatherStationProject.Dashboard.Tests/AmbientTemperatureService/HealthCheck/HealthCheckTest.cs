using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MockQueryable.Moq;
using Moq;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.AmbientTemperatureService.HealthCheck;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AmbientTemperatureService
{
    public class HealthCheckTest
    {
        [Fact]
        public async Task When_Getting_GoodStatus_Should_Return_Healthy()
        {
            // Arrange
            var mockDbSet = new List<AmbientTemperature>().AsQueryable().BuildMockDbSet();
            var mockDbContext = new Mock<AmbientTemperatureDbContext>();
            mockDbContext.Setup(x => x.AmbientTemperatures).Returns(mockDbSet.Object);

            // Act
            var result =
                await new HealthCheck(mockDbContext.Object).CheckHealthAsync(new HealthCheckContext(),
                    CancellationToken.None);

            // Assert
            Assert.Equal(HealthStatus.Healthy, result.Status);
        }
        
        [Fact]
        public async Task When_Getting_WrongStatus_Should_Return_UnHealthy()
        {
            // Arrange
            var mockDbContext = new Mock<AmbientTemperatureDbContext>();
            mockDbContext.Setup(x => x.AmbientTemperatures).Throws(new Exception());

            // Act
            var result =
                await new HealthCheck(mockDbContext.Object).CheckHealthAsync(new HealthCheckContext(),
                    CancellationToken.None);

            // Assert
            Assert.Equal(HealthStatus.Unhealthy, result.Status);
        }
    }
}