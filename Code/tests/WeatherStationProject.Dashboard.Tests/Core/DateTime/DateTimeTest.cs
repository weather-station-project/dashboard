using System;
using WeatherStationProject.Dashboard.Core.DateTime;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.Core.DateTime;

public class DateTimeTest
{
    [Fact]
    public void When_ConvertingDate_Given_LocalDate_Should_Return_ExpectedResult()
    {
        // Arrange
        var date = new System.DateTime(2022, 1, 1, 3, 5, 4, DateTimeKind.Utc);

        // Act & Assert
        Assert.Equal(date, DateTimeConverter.ConvertToUtc(date).ToUniversalTime());
    }
}