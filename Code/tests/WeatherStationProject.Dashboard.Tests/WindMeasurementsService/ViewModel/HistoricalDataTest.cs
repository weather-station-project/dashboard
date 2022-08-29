using System;
using System.Collections.Generic;
using System.Linq;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;
using WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.WindMeasurementsService
{
    public class HistoricalDataDtoTest
    {
        private readonly WindMeasurements _m1 = new() {Speed = 10, Direction = "N", DateTime = new DateTime(2022, 01, 01, 5, 0, 0)};
        private readonly WindMeasurements _m2 = new() {Speed = 20, Direction = "E", DateTime = new DateTime(2022, 01, 01, 5, 30, 0)};
        private readonly WindMeasurements _m3 = new() {Speed = 30, Direction = "N", DateTime = new DateTime(2022, 01, 01, 5, 45, 0)};
        private readonly WindMeasurements _m4 = new() {Speed = 50, Direction = "O", DateTime = new DateTime(2023, 02, 15, 1, 15, 0)};
        private readonly WindMeasurements _m5 = new() {Speed = 60, Direction = "NO", DateTime = new DateTime(2023, 02, 15, 1, 45, 0)};
        private readonly WindMeasurements _m6 = new() {Speed = 70, Direction = "NO", DateTime = new DateTime(2023, 02, 15, 1, 55, 0)};
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<WindMeasurements>() {_m1, _m2, _m3, _m4, _m5, _m6}, GroupingValues.Hours, 
                false, false);
            
            // Assert
            Assert.Null(result.SummaryByGroupingItem);
            Assert.Null(result.Measurements);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_WithMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<WindMeasurements>() {_m1, _m2, _m3, _m4, _m5, _m6}, GroupingValues.Hours, 
                false, true);
            
            // Assert
            Assert.Null(result.SummaryByGroupingItem);
            Assert.NotEmpty(result.Measurements);
            Assert.Equal(6 , result.Measurements.Count);
            Assert.Equal(_m1.Speed , ((WindMeasurementsDto)result.Measurements[0]).Speed);
        }
        
        [Theory]
        [InlineData("2022-01-01/05", "2023-02-15/01", GroupingValues.Hours)]
        [InlineData("2022-01-01", "2023-02-15", GroupingValues.Days)]
        [InlineData("2022-01", "2023-02", GroupingValues.Months)]
        public void When_BuildingDto_Given_Measurements_And_Grouping_And_WithSummary_NoMeasurements_Should_Return_ExpectedData(string keyGroup1,
            string keyGroup2, GroupingValues groupingValues)
        {
            // Act
            var result = new HistoricalDataDto(new List<WindMeasurements>() {_m1, _m2, _m3, _m4, _m5, _m6}, groupingValues, 
                true, false);
            
            // Assert
            var keyGroup1Item = result.SummaryByGroupingItem.FirstOrDefault(x => x.Key == keyGroup1);
            var keyGroup2Item = result.SummaryByGroupingItem.FirstOrDefault(x => x.Key == keyGroup2);
            
            Assert.NotNull(keyGroup1Item);
            Assert.NotNull(keyGroup2Item);
            Assert.Null(result.Measurements);
            Assert.NotEmpty(result.SummaryByGroupingItem);
            Assert.Equal(new[]{"N", "NO"}, result.PredominantWindDirections.Keys);
            Assert.Equal(new[]{2, 2}, result.PredominantWindDirections.Values);

            if (keyGroup1Item != null)
            {
                Assert.Equal((_m1.Speed + _m2.Speed + _m3.Speed) / 3, keyGroup1Item.AvgSpeed);
                Assert.Equal(_m3.Speed, keyGroup1Item.MaxGust);
            }

            if (keyGroup2Item != null)
            {
                Assert.Equal((_m4.Speed + _m5.Speed + _m6.Speed) / 3, keyGroup2Item.AvgSpeed);
                // Assert.Equal(_m6.Direction, keyGroup2Item.PredominantDirection);
                Assert.Equal(_m6.Speed, keyGroup2Item.MaxGust);
            }
        }
    }
}