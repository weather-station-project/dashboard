using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MockQueryable.Moq;
using Moq;
using WeatherStationProject.Dashboard.RainfallService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.RainfallService.Tests
{
    public class HealthCheckTest
    {
        [Fact]
        public async Task When_Getting_GoodStatus_Should_Return_Healthy()
        {
            // Arrange
            var mockDbSet = new List<Rainfall>().AsQueryable().BuildMockDbSet();
            var mockDbContext = new Mock<RainfallDbContext>();
            mockDbContext.Setup(x => x.Rainfall).Returns(mockDbSet.Object);

            // Act
            var result =
                await new HealthCheck.HealthCheck(mockDbContext.Object).CheckHealthAsync(new HealthCheckContext(),
                    CancellationToken.None);

            // Assert
            Assert.Equal(HealthStatus.Healthy, result.Status);
        }
        
        [Fact]
        public async Task When_Getting_WrongStatus_Should_Return_UnHealthy()
        {
            // Arrange
            var mockDbContext = new Mock<RainfallDbContext>();
            mockDbContext.Setup(x => x.Rainfall).Throws(new Exception());

            // Act
            var result =
                await new HealthCheck.HealthCheck(mockDbContext.Object).CheckHealthAsync(new HealthCheckContext(),
                    CancellationToken.None);

            // Assert
            Assert.Equal(HealthStatus.Unhealthy, result.Status);
        }
    }
}