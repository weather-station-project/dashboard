using System;
using System.Collections.Generic;
using System.Linq;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;
using WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.GroundTemperatureService;

public class HistoricalDataDtoTest
{
    private readonly GroundTemperature _m1 = new() {Temperature = 25, DateTime = new DateTime(2022, 01, 01, 5, 0, 0)};
    private readonly GroundTemperature _m2 = new() {Temperature = 30, DateTime = new DateTime(2022, 01, 01, 5, 30, 0)};
    private readonly GroundTemperature _m3 = new() {Temperature = 80, DateTime = new DateTime(2023, 02, 15, 1, 15, 0)};
    private readonly GroundTemperature _m4 = new() {Temperature = 110, DateTime = new DateTime(2023, 02, 15, 1, 45, 0)};

    [Fact]
    public void
        When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_NoMeasurements_Should_Return_ExpectedData()
    {
        // Act
        var result = new HistoricalDataDto(new List<GroundTemperature> {_m1, _m2, _m3, _m4}, GroupingValues.Hours,
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
        var result = new HistoricalDataDto(new List<GroundTemperature> {_m1, _m2, _m3, _m4}, GroupingValues.Hours,
            false, true);

        // Assert
        Assert.Null(result.SummaryByGroupingItem);
        Assert.NotEmpty(result.Measurements);
        Assert.Equal(4, result.Measurements.Count);
        Assert.Equal(_m1.Temperature, ((GroundTemperatureDto) result.Measurements[0]).Temperature);
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
        var result = new HistoricalDataDto(new List<GroundTemperature> {_m1, _m2, _m3, _m4}, groupingValues,
            true, false);

        // Assert
        var keyGroup1Item = result.SummaryByGroupingItem.FirstOrDefault(x => x.Key == keyGroup1);
        var keyGroup2Item = result.SummaryByGroupingItem.FirstOrDefault(x => x.Key == keyGroup2);

        Assert.NotNull(keyGroup1Item);
        Assert.NotNull(keyGroup2Item);
        Assert.Null(result.Measurements);
        Assert.NotEmpty(result.SummaryByGroupingItem);

        if (keyGroup1Item != null)
        {
            Assert.Equal(_m2.Temperature, keyGroup1Item.MaxTemperature);
            Assert.Equal((_m1.Temperature + _m2.Temperature) / 2, keyGroup1Item.AvgTemperature);
            Assert.Equal(_m1.Temperature, keyGroup1Item.MinTemperature);
        }

        if (keyGroup2Item != null)
        {
            Assert.Equal(_m4.Temperature, keyGroup2Item.MaxTemperature);
            Assert.Equal((_m3.Temperature + _m4.Temperature) / 2, keyGroup2Item.AvgTemperature);
            Assert.Equal(_m3.Temperature, keyGroup2Item.MinTemperature);
        }
    }
}