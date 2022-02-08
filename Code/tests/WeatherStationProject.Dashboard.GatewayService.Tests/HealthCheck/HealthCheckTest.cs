using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Moq.Protected;
using Xunit;

namespace WeatherStationProject.Dashboard.GatewayService.Tests
{
    public class HealthCheckTest
    {
        [Fact]
        public async Task When_Getting_GoodStatus_Should_Return_Healthy()
        {
            // Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage _, CancellationToken _) =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    
                    return response;
                });

            // Act
            var result =
                await new HealthCheck.HealthCheck(mockMessageHandler.Object).CheckHealthAsync(new HealthCheckContext(),
                    CancellationToken.None);

            // Assert
            Assert.Equal(HealthStatus.Healthy, result.Status);
        }
        
        [Fact]
        public async Task When_Getting_WrongStatus_Should_Return_UnHealthy()
        {
            // Arrange
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage _, CancellationToken _) =>
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    
                    return response;
                });

            // Act
            var result =
                await new HealthCheck.HealthCheck(mockMessageHandler.Object).CheckHealthAsync(new HealthCheckContext(),
                    CancellationToken.None);

            // Assert
            Assert.Equal(HealthStatus.Unhealthy, result.Status);
        }
    }
}