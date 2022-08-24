using System;
using System.Collections.Generic;
using System.Linq;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.RainfallService.Data;
using WeatherStationProject.Dashboard.RainfallService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.RainfallService
{
    public class HistoricalDataDtoTest
    {
        private readonly Rainfall _m1 = new() {Amount = 25, DateTime = new DateTime(2022, 01, 01, 5, 0, 0)};
        private readonly Rainfall _m2 = new() {Amount = 30, DateTime = new DateTime(2022, 01, 01, 5, 30, 0)};
        private readonly Rainfall _m3 = new() {Amount = 80, DateTime = new DateTime(2023, 02, 15, 1, 15, 0)};
        private readonly Rainfall _m4 = new() {Amount = 118, DateTime = new DateTime(2023, 02, 15, 1, 45, 0)};

        [Fact]
        public void
            When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<Rainfall>() {_m1, _m2, _m3, _m4}, GroupingValues.Hours,
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
            var result = new HistoricalDataDto(new List<Rainfall>() {_m1, _m2, _m3, _m4}, GroupingValues.Hours,
                false, true);

            // Assert
            Assert.Null(result.SummaryByGroupingItem);
            Assert.NotEmpty(result.Measurements);
            Assert.Equal(4, result.Measurements.Count);
            Assert.Equal(_m1.Amount, ((RainfallDto) result.Measurements[0]).Amount);
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
            var result = new HistoricalDataDto(new List<Rainfall>() {_m1, _m2, _m3, _m4}, groupingValues,
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
                Assert.Equal(_m2.Amount, keyGroup1Item.MaxAmount);
                Assert.Equal((_m1.Amount + _m2.Amount) / 2, keyGroup1Item.AvgAmount);
                Assert.Equal(_m1.Amount, keyGroup1Item.MinAmount);
            }

            if (keyGroup2Item != null)
            {
                Assert.Equal(_m4.Amount, keyGroup2Item.MaxAmount);
                Assert.Equal((_m3.Amount + _m4.Amount) / 2, keyGroup2Item.AvgAmount);
                Assert.Equal(_m3.Amount, keyGroup2Item.MinAmount);
            }
        }
    }
}