using System;
using System.Collections.Generic;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel;
using WeatherStationProject.Dashboard.Data.Validations;
using Xunit;
using HistoricalDataDto = WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel.HistoricalDataDto;

namespace WeatherStationProject.Dashboard.Tests.AmbientTemperatureService
{
    public class HistoricalDtoTest
    {
        private readonly AmbientTemperature _m1 = new() {Temperature = 10, DateTime = new DateTime(2022, 01, 01, 5, 0, 0)};
        private readonly AmbientTemperature _m2 = new() {Temperature = 20, DateTime = new DateTime(2022, 01, 01, 5, 30, 0)};
        private readonly AmbientTemperature _m3 = new() {Temperature = 50, DateTime = new DateTime(2023, 02, 15, 1, 15, 0)};
        private readonly AmbientTemperature _m4 = new() {Temperature = 60, DateTime = new DateTime(2023, 02, 15, 1, 45, 0)};
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AmbientTemperature>() {_m1, _m2, _m3, _m4}, GroupingValues.Hours, 
                false, false);
            
            // Assert
            Assert.Null(result.SummaryByGroupingItem);
            Assert.Null(result.Measurements);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_WithMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AmbientTemperature>() {_m1, _m2, _m3, _m4}, GroupingValues.Hours, 
                false, true);
            
            // Assert
            Assert.Null(result.SummaryByGroupingItem);
            Assert.NotEmpty(result.Measurements);
            Assert.Equal(4 , result.Measurements.Count);
            Assert.Equal(_m1.Temperature , ((AmbientTemperatureDto)result.Measurements[0]).Temperature);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingHours_And_WithSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AmbientTemperature>() {_m1, _m2, _m3, _m4}, GroupingValues.Hours, 
                true, false);
            
            // Assert
            Assert.Null(result.Measurements);
            Assert.NotEmpty(result.SummaryByGroupingItem);
            Assert.Equal((_m1.Temperature + _m2.Temperature) / 2, result.SummaryByGroupingItem["2022-01-01/05"].TemperatureAvg);
            Assert.Equal((_m3.Temperature + _m4.Temperature) / 2, result.SummaryByGroupingItem["2023-02-15/01"].TemperatureAvg);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingDays_And_WithSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AmbientTemperature>() {_m1, _m2, _m3, _m4}, GroupingValues.Days, 
                true, false);
            
            // Assert
            Assert.Null(result.Measurements);
            Assert.NotEmpty(result.SummaryByGroupingItem);
            Assert.Equal((_m1.Temperature + _m2.Temperature) / 2, result.SummaryByGroupingItem["2022-01-01"].TemperatureAvg);
            Assert.Equal((_m3.Temperature + _m4.Temperature) / 2, result.SummaryByGroupingItem["2023-02-15"].TemperatureAvg);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingMonths_And_WithSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AmbientTemperature>() {_m1, _m2, _m3, _m4}, GroupingValues.Months, 
                true, false);
            
            // Assert
            Assert.Null(result.Measurements);
            Assert.NotEmpty(result.SummaryByGroupingItem);
            Assert.Equal((_m1.Temperature + _m2.Temperature) / 2, result.SummaryByGroupingItem["2022-01"].TemperatureAvg);
            Assert.Equal((_m3.Temperature + _m4.Temperature) / 2, result.SummaryByGroupingItem["2023-02"].TemperatureAvg);
        }
    }
}