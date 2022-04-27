using System;
using System.Net;
using WeatherStationProject.Dashboard.AuthenticationService.Controllers;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AuthenticationService
{
    public class AuthenticationControllerTest
    {
        [Fact]
        public void When_Getting_Token_Given_Wrong_Credentials_Should_Return_403()
        {
            // Arrange
            Environment.SetEnvironmentVariable("AUTHENTICATION_SECRET", "test");

            // Act
            var controller = new AuthenticationController();
            var response = controller.Index("1234");

            // Assert
            Assert.Equal((int) HttpStatusCode.Forbidden, response.StatusCode);
            Assert.Equal("Secret invalid!", response.Value);
        }

        [Fact]
        public void When_Getting_Token_Given_Good_Credentials_Should_Return_JWT()
        {
            // Arrange
            var secret = "test";
            Environment.SetEnvironmentVariable("AUTHENTICATION_SECRET", secret);
            Environment.SetEnvironmentVariable("AUDIENCE_SECRET", "audience");

            // Act
            var controller = new AuthenticationController();
            var response = controller.Index(secret);

            // Assert
            Assert.Equal((int) HttpStatusCode.OK, response.StatusCode);
        }
    }
}