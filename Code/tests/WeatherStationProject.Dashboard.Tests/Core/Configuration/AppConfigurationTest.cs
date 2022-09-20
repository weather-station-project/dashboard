using System;
using WeatherStationProject.Dashboard.Core.Configuration;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.Core;

public class AppConfigurationTest
{
    [Fact]
    public void When_GettingDatabaseConnectionString_Should_Return_ExpectedResult()
    {
        // Arrange
        const string server = "testServer";
        const string database = "database";
        const string user = "user";
        const string password = "password";

        Environment.SetEnvironmentVariable("SERVER", server);
        Environment.SetEnvironmentVariable("DATABASE", database);
        Environment.SetEnvironmentVariable("USER", user);
        Environment.SetEnvironmentVariable("PASSWORD", password);

        var expectedValue = $"Host={server};Database={database};Username={user};Password={password}";

        // Act & Assert
        Assert.Equal(AppConfiguration.DatabaseConnectionString, expectedValue);
    }

    [Fact]
    public void When_GettingAuthenticationSecret_Should_Return_ExpectedResult()
    {
        // Arrange
        const string value = "testAuthSecret";
        Environment.SetEnvironmentVariable("AUTHENTICATION_SECRET", value);

        // Act & Assert
        Assert.Equal(AppConfiguration.AuthenticationSecret, value);
    }

    [Fact]
    public void When_GettingAccuWeatherApiKey_Should_Return_ExpectedResult()
    {
        // Arrange
        const string value = "testApiKey";
        Environment.SetEnvironmentVariable("ACCUWEATHER_API_KEY", value);

        // Act & Assert
        Assert.Equal(AppConfiguration.AccuWeatherApiKey, value);
    }

    [Fact]
    public void When_GettingAccuWeatherLocationName_Should_Return_ExpectedResult()
    {
        // Arrange
        const string value = "testLocationName";
        Environment.SetEnvironmentVariable("ACCUWEATHER_LOCATION_NAME", value);

        // Act & Assert
        Assert.Equal(AppConfiguration.AccuWeatherLocationName, value);
    }

    [Fact]
    public void When_GettingAccuWeatherApiHost_Should_Return_ExpectedResult()
    {
        // Arrange
        const string value = "testApiHost";
        Environment.SetEnvironmentVariable("WEATHER_API_HOST", value);

        // Act & Assert
        Assert.Equal(AppConfiguration.WeatherApiHost, value);
    }

    [Fact]
    public void When_GettingAuthenticationServiceHost_Should_Return_ExpectedResult()
    {
        // Arrange
        const string value = "testServiceHost";
        Environment.SetEnvironmentVariable("AUTHENTICATION_SERVICE_HOST", value);

        // Act & Assert
        Assert.Equal(AppConfiguration.AuthenticationServiceHost, value);
    }
}