using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Moq.Protected;
using WeatherStationProject.Dashboard.GatewayService.HealthCheck;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.GatewayService;

public class HealthCheckTest
{
    [Fact]
    public async Task When_Getting_GoodStatus_Should_Return_Healthy()
    {
        // Act
        var result =
            await new HealthCheck().CheckHealthAsync(new HealthCheckContext(),
                CancellationToken.None);

        // Assert
        Assert.Equal(HealthStatus.Healthy, result.Status);
    }
}