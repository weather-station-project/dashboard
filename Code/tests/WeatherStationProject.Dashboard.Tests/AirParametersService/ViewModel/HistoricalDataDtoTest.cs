using System;
using System.Collections.Generic;
using System.Linq;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.AirParametersService.ViewModel;
using WeatherStationProject.Dashboard.Data.Validations;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AirParametersService;

public class HistoricalDataDtoTest
{
    private readonly AirParameters _m1 = new()
        {Humidity = 10, Pressure = 20, DateTime = new DateTime(2022, 01, 01, 5, 0, 0)};

    private readonly AirParameters _m2 = new()
        {Humidity = 20, Pressure = 30, DateTime = new DateTime(2022, 01, 01, 5, 30, 0)};

    private readonly AirParameters _m3 = new()
        {Humidity = 50, Pressure = 80, DateTime = new DateTime(2023, 02, 15, 1, 15, 0)};

    private readonly AirParameters _m4 = new()
        {Humidity = 60, Pressure = 90, DateTime = new DateTime(2023, 02, 15, 1, 45, 0)};

    [Fact]
    public void
        When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_NoMeasurements_Should_Return_ExpectedData()
    {
        // Act
        var result = new HistoricalDataDto(new List<AirParameters> {_m1, _m2, _m3, _m4}, GroupingValues.Hours,
            false, false);

        // Assert
        Assert.Null(result.SummaryByGroupingItem);
        Assert.Null(result.Measurements);
    }

    [Fact]
    public void
        When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_WithMeasurements_Should_Return_ExpectedData()
    {
        // Act
        var result = new HistoricalDataDto(new List<AirParameters> {_m1, _m2, _m3, _m4}, GroupingValues.Hours,
            false, true);

        // Assert
        Assert.Null(result.SummaryByGroupingItem);
        Assert.NotEmpty(result.Measurements);
        Assert.Equal(4, result.Measurements.Count);
        Assert.Equal(_m1.Humidity, ((AirParametersDto) result.Measurements[0]).Humidity);
    }

    [Theory]
    [InlineData("2022-01-01/05", "2023-02-15/01", GroupingValues.Hours)]
    [InlineData("2022-01-01", "2023-02-15", GroupingValues.Days)]
    [InlineData("2022-01", "2023-02", GroupingValues.Months)]
    public void
        When_BuildingDto_Given_Measurements_And_Grouping_And_WithSummary_NoMeasurements_Should_Return_ExpectedData(
            string keyGroup1,
            string keyGroup2, GroupingValues groupingValues)
    {
        // Act
        var result = new HistoricalDataDto(new List<AirParameters> {_m1, _m2, _m3, _m4}, groupingValues, true, false);

        // Assert
        var keyGroup1Item = result.SummaryByGroupingItem.FirstOrDefault(x => x.Key == keyGroup1);
        var keyGroup2Item = result.SummaryByGroupingItem.FirstOrDefault(x => x.Key == keyGroup2);

        Assert.NotNull(keyGroup1Item);
        Assert.NotNull(keyGroup2Item);
        Assert.Null(result.Measurements);
        Assert.NotEmpty(result.SummaryByGroupingItem);

        if (keyGroup1Item != null)
        {
            Assert.Equal(_m2.Humidity, keyGroup1Item.MaxHumidity);
            Assert.Equal(_m2.Pressure, keyGroup1Item.MaxPressure);
            Assert.Equal((_m1.Humidity + _m2.Humidity) / 2, keyGroup1Item.AvgHumidity);
            Assert.Equal((_m1.Pressure + _m2.Pressure) / 2, keyGroup1Item.AvgPressure);
            Assert.Equal(_m1.Humidity, keyGroup1Item.MinHumidity);
            Assert.Equal(_m1.Pressure, keyGroup1Item.MinPressure);
        }

        if (keyGroup2Item != null)
        {
            Assert.Equal(_m4.Humidity, keyGroup2Item.MaxHumidity);
            Assert.Equal(_m4.Pressure, keyGroup2Item.MaxPressure);
            Assert.Equal((_m3.Humidity + _m4.Humidity) / 2, keyGroup2Item.AvgHumidity);
            Assert.Equal((_m3.Pressure + _m4.Pressure) / 2, keyGroup2Item.AvgPressure);
            Assert.Equal(_m3.Humidity, keyGroup2Item.MinHumidity);
            Assert.Equal(_m3.Pressure, keyGroup2Item.MinPressure);
        }
    }
}