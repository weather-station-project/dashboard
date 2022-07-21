using System;
using System.Collections.Generic;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.AirParametersService.ViewModel;
using WeatherStationProject.Dashboard.Data.Validations;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AirParametersService
{
    public class HistoricalDataDtoTest
    {
        private readonly AirParameters _m1 = new() {Humidity = 10, Pressure = 20, DateTime = new DateTime(2022, 01, 01, 5, 0, 0)};
        private readonly AirParameters _m2 = new() {Humidity = 20, Pressure = 30, DateTime = new DateTime(2022, 01, 01, 5, 30, 0)};
        private readonly AirParameters _m3 = new() {Humidity = 50, Pressure = 80, DateTime = new DateTime(2023, 02, 15, 1, 15, 0)};
        private readonly AirParameters _m4 = new() {Humidity = 60, Pressure = 90, DateTime = new DateTime(2023, 02, 15, 1, 45, 0)};
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AirParameters>() {_m1, _m2, _m3, _m4}, GroupingValues.Hours, 
                false, false);
            
            // Assert
            Assert.Null(result.SummaryByGroupingItem);
            Assert.Null(result.Measurements);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingHours_And_NoSummary_WithMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AirParameters>() {_m1, _m2, _m3, _m4}, GroupingValues.Hours, 
                false, true);
            
            // Assert
            Assert.Null(result.SummaryByGroupingItem);
            Assert.NotEmpty(result.Measurements);
            Assert.Equal(4 , result.Measurements.Count);
            Assert.Equal(_m1.Humidity , ((AirParametersDto)result.Measurements[0]).Humidity);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingHours_And_WithSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AirParameters>() {_m1, _m2, _m3, _m4}, GroupingValues.Hours, 
                true, false);
            
            // Assert
            Assert.Null(result.Measurements);
            Assert.NotEmpty(result.SummaryByGroupingItem);
            Assert.Equal((_m1.Humidity + _m2.Humidity) / 2, result.SummaryByGroupingItem["2022-01-01/05"].HumidityAvg);
            Assert.Equal((_m1.Pressure + _m2.Pressure) / 2, result.SummaryByGroupingItem["2022-01-01/05"].PressureAvg);
            Assert.Equal((_m3.Humidity + _m4.Humidity) / 2, result.SummaryByGroupingItem["2023-02-15/01"].HumidityAvg);
            Assert.Equal((_m3.Pressure + _m4.Pressure) / 2, result.SummaryByGroupingItem["2023-02-15/01"].PressureAvg);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingDays_And_WithSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AirParameters>() {_m1, _m2, _m3, _m4}, GroupingValues.Days, 
                true, false);
            
            // Assert
            Assert.Null(result.Measurements);
            Assert.NotEmpty(result.SummaryByGroupingItem);
            Assert.Equal((_m1.Humidity + _m2.Humidity) / 2, result.SummaryByGroupingItem["2022-01-01"].HumidityAvg);
            Assert.Equal((_m1.Pressure + _m2.Pressure) / 2, result.SummaryByGroupingItem["2022-01-01"].PressureAvg);
            Assert.Equal((_m3.Humidity + _m4.Humidity) / 2, result.SummaryByGroupingItem["2023-02-15"].HumidityAvg);
            Assert.Equal((_m3.Pressure + _m4.Pressure) / 2, result.SummaryByGroupingItem["2023-02-15"].PressureAvg);
        }
        
        [Fact]
        public void When_BuildingDto_Given_Measurements_And_GroupingMonths_And_WithSummary_NoMeasurements_Should_Return_ExpectedData()
        {
            // Act
            var result = new HistoricalDataDto(new List<AirParameters>() {_m1, _m2, _m3, _m4}, GroupingValues.Months, 
                true, false);
            
            // Assert
            Assert.Null(result.Measurements);
            Assert.NotEmpty(result.SummaryByGroupingItem);
            Assert.Equal((_m1.Humidity + _m2.Humidity) / 2, result.SummaryByGroupingItem["2022-01"].HumidityAvg);
            Assert.Equal((_m1.Pressure + _m2.Pressure) / 2, result.SummaryByGroupingItem["2022-01"].PressureAvg);
            Assert.Equal((_m3.Humidity + _m4.Humidity) / 2, result.SummaryByGroupingItem["2023-02"].HumidityAvg);
            Assert.Equal((_m3.Pressure + _m4.Pressure) / 2, result.SummaryByGroupingItem["2023-02"].PressureAvg);
        }
    }
}