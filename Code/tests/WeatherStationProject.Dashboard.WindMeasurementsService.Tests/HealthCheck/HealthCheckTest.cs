using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MockQueryable.Moq;
using Moq;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Tests
{
    public class HealthCheckTest
    {
        [Fact]
        public async Task When_Getting_GoodStatus_Should_Return_Healthy()
        {
            // Arrange
            var mockDbSet = new List<WindMeasurements>().AsQueryable().BuildMockDbSet();
            var mockDbContext = new Mock<WindMeasurementsDbContext>();
            mockDbContext.Setup(x => x.WindMeasurements).Returns(mockDbSet.Object);

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
            var mockDbContext = new Mock<WindMeasurementsDbContext>();
            mockDbContext.Setup(x => x.WindMeasurements).Throws(new Exception());

            // Act
            var result =
                await new HealthCheck.HealthCheck(mockDbContext.Object).CheckHealthAsync(new HealthCheckContext(),
                    CancellationToken.None);

            // Assert
            Assert.Equal(HealthStatus.Unhealthy, result.Status);
        }
    }
}