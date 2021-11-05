using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using WeatherStationProject.Dashboard.App.Controllers;
using Xunit;

namespace WeatherStationProject.Dashboard.App.Tests
{
    public class AccuWeatherProxyControllerTest
    {
        private const string MockResult = "mockResult";
        
        [Fact]
        public async Task When_GettingLocationKey_Should_Return_ExpectedResult()
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
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent(JsonConvert.SerializeObject(MockResult));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            var controller = new AccuWeatherProxyController(mockMessageHandler.Object);

            // Act
            var locationKey = await controller.GetLocationKey("");

            // Assert
            Assert.Equal('"' + MockResult + '"', locationKey);
        }

        [Fact]
        public async Task When_GettingCurrentConditions_Should_Return_ExpectedResult()
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
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent(JsonConvert.SerializeObject(MockResult));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            var controller = new AccuWeatherProxyController(mockMessageHandler.Object);

            // Act
            var locationKey = await controller.GetCurrentConditions("", "");

            // Assert
            Assert.Equal('"' + MockResult + '"', locationKey);
        }
        
        [Fact]
        public async Task When_GettingForecast_Should_Return_ExpectedResult()
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
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent(JsonConvert.SerializeObject(MockResult));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            var controller = new AccuWeatherProxyController(mockMessageHandler.Object);

            // Act
            var locationKey = await controller.GetForecast("", "");

            // Assert
            Assert.Equal('"' + MockResult + '"', locationKey);
        }
        
        [Fact]
        public async Task When_GettingForecast_Given_HttpError_Should_ThrowException()
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
            var controller = new AccuWeatherProxyController(mockMessageHandler.Object);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => controller.GetForecast("", ""));
        }
    }
}