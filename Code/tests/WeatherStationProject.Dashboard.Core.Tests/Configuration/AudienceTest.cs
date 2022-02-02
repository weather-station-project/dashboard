using System;
using WeatherStationProject.Dashboard.Core.Configuration;
using Xunit;

namespace WeatherStationProject.Dashboard.Core.Tests
{
    public class AudienceTest
    {
        [Fact]
        public void When_GettingAudienceSecret_Should_Return_ExpectedResult()
        {
            // Arrange
            const string value = "testSecret";
            Environment.SetEnvironmentVariable("AUDIENCE_SECRET", value);

            // Act & Assert
            Assert.Equal(Audience.Secret, value);
        }

        [Fact]
        public void When_GettingIssuer_Should_Return_ExpectedResult()
        {
            // Arrange
            const string value = "Weather Station Project";

            // Act & Assert
            Assert.Equal(Audience.Issuer, value);
        }

        [Fact]
        public void When_GettingValidAudience_Should_Return_ExpectedResult()
        {
            // Arrange
            const string value = "localhost";

            // Act & Assert
            Assert.Equal(Audience.ValidAudience, value);
        }
    }
}