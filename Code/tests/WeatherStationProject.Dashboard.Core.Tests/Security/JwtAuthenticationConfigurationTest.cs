using System;
using Microsoft.IdentityModel.Tokens;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Core.Security;
using Xunit;

namespace WeatherStationProject.Dashboard.Core.Tests
{
    public class JwtAuthenticationConfigurationTest
    {
        [Fact]
        public void When_GettingTokenValidationParameters_Should_Return_ExpectedResult()
        {
            // Arrange
            const string key = "testSigningKey";

            Environment.SetEnvironmentVariable("AUDIENCE_SECRET", key);

            var expectedValue = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidIssuer = Audience.Issuer,
                ValidateAudience = true,
                ValidAudience = Audience.ValidAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };

            // Act & Assert
            var parameters = JwtAuthenticationConfiguration.GetTokenValidationParameters();
            Assert.Equal(parameters.ValidateIssuerSigningKey, expectedValue.ValidateIssuerSigningKey);
            Assert.Equal(parameters.ValidateIssuer, expectedValue.ValidateIssuer);
            Assert.Equal(parameters.ValidIssuer, expectedValue.ValidIssuer);
            Assert.Equal(parameters.ValidateAudience, expectedValue.ValidateAudience);
            Assert.Equal(parameters.ValidAudience, expectedValue.ValidAudience);
            Assert.Equal(parameters.ValidateLifetime, expectedValue.ValidateLifetime);
            Assert.Equal(parameters.ClockSkew, expectedValue.ClockSkew);
            Assert.Equal(parameters.RequireExpirationTime, expectedValue.RequireExpirationTime);
        }
    }
}