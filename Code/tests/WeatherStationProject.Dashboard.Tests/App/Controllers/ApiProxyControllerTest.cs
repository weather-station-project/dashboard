using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using WeatherStationProject.Dashboard.App.Controllers;
using WeatherStationProject.Dashboard.Core.Model;
using WeatherStationProject.Dashboard.Data.Validations;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.App
{
    public class ApiProxyControllerTest
    {
        private static readonly AuthenticationToken MockAuth = new()
        {
            AccessToken = "test",
            ExpiresIn = 1
        };

        [Fact]
        public async Task When_GettingLastMeasurement_Should_Return_ExpectedResult()
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
                    response.Content = new StringContent(JsonConvert.SerializeObject(MockAuth));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            var controller = new ApiProxyController(mockMessageHandler.Object);
            Environment.SetEnvironmentVariable("AUTHENTICATION_SERVICE_HOST", "http://127.0.0.1");
            Environment.SetEnvironmentVariable("WEATHER_API_HOST", "http://127.0.0.1");
            Environment.SetEnvironmentVariable("AUTHENTICATION_SECRET", "123456");

            // Act
            var result = await controller.LastMeasurements();

            // Assert
            Assert.Equal("{\"AccessToken\":\"test\",\"ExpiresIn\":1}", result.Value);
        }

        [Fact]
        public async Task When_GettingLastMeasurement_Given_HttpError_Should_ThrowException()
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
            var controller = new ApiProxyController(mockMessageHandler.Object);

            // Act
            var result = await controller.LastMeasurements();

            // Assert
            Assert.Equal("Auth token could not be retrieved", result.Value);
        }

        [Fact]
        public async Task When_GettingHistoricalData_Should_Return_ExpectedResult()
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
                    response.Content = new StringContent(JsonConvert.SerializeObject(MockAuth));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            var controller = new ApiProxyController(mockMessageHandler.Object);
            Environment.SetEnvironmentVariable("AUTHENTICATION_SERVICE_HOST", "http://127.0.0.1");
            Environment.SetEnvironmentVariable("WEATHER_API_HOST", "http://127.0.0.1");
            Environment.SetEnvironmentVariable("AUTHENTICATION_SECRET", "123456");

            // Act
            var result = await controller.HistoricalData(DateTime.Now, DateTime.Now, GroupingValues.Hours.ToString(),
                false, false);

            // Assert
            Assert.Equal("{\"AccessToken\":\"test\",\"ExpiresIn\":1}", result.Value);
        }

        [Fact]
        public async Task When_GettingHistorialData_Given_HttpError_Should_ThrowException()
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
            var controller = new ApiProxyController(mockMessageHandler.Object);

            // Act
            var result = await controller.HistoricalData(DateTime.Now, DateTime.Now, GroupingValues.Hours.ToString(),
                false, false);

            // Assert
            Assert.Equal("Auth token could not be retrieved", result.Value);
        }
    }
}