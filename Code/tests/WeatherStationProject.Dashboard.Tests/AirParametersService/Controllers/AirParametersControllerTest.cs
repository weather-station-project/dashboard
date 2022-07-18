using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.AirParametersService.Controllers;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.AirParametersService.Services;
using WeatherStationProject.Dashboard.AirParametersService.ViewModel;
using WeatherStationProject.Dashboard.Data.Validations;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AirParametersService
{
    public class AirParametersControllerTest
    {
        [Fact]
        public async Task When_Getting_LastMeasurement_Given_Null_Should_Return_Not_Found_Response()
        {
            // Arrange
            var parametersService = new Mock<IAirParametersService>();
            parametersService.Setup(x => x.GetLastAirParameters()).Returns(Task.FromResult<AirParameters>(null!));
            var controller = new AirParametersController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new NotFoundResult().GetType(), response.Result);
        }

        [Fact]
        public async Task When_Getting_LastMeasurement_Given_Result_Should_Return_RelatedDto()
        {
            // Arrange
            var measurement = new AirParameters {Humidity = 5};
            var parametersService = new Mock<IAirParametersService>();
            parametersService.Setup(x => x.GetLastAirParameters()).Returns(Task.FromResult(measurement));
            var controller = new AirParametersController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new AirParametersDTO().GetType(), response.Value);
            if (response.Value != null)
                Assert.Equal(AirParametersDTO.FromEntity(measurement).Humidity, response.Value.Humidity);
        }
        
        [Fact]
        public async Task When_Getting_HistoricalData_Given_Empty_Should_Return_Not_Found_Response()
        {
            // Arrange
            var parametersService = new Mock<IAirParametersService>();
            parametersService.Setup(x => x.GetAirParametersBetweenDates(It.IsAny<DateTime>(), 
                It.IsAny<DateTime>())).Returns(Task.FromResult(new List<AirParameters>()));
            var controller = new AirParametersController(parametersService.Object);

            // Act
            var response = await controller.HistoricalData(DateTime.Now, 
                DateTime.Now, GroupingValues.Days.ToString(), false, false);

            // Assert
            Assert.IsType(new NotFoundResult().GetType(), response.Result);
        }
        
        [Fact]
        public async Task When_Getting_HistoricalData_Given_Result_Should_Return_Expected_Response()
        {
            // Arrange
            var measurement = new AirParameters
            {
                Humidity = 2,
                Pressure = 80,
                DateTime = DateTime.UtcNow
            };
            var parametersService = new Mock<IAirParametersService>();
            parametersService.Setup(x => x.GetAirParametersBetweenDates(It.IsAny<DateTime>(), 
                It.IsAny<DateTime>())).Returns(Task.FromResult(new List<AirParameters> {measurement}));
            var controller = new AirParametersController(parametersService.Object);

            // Act
            var response = await controller.HistoricalData(DateTime.Now, 
                DateTime.Now, GroupingValues.Days.ToString(), false, false);

            // Assert
            Assert.IsType(new HistoricalDataDTO(new List<AirParameters> {measurement}, GroupingValues.Days, false, false).GetType(), response.Value);
        }
    }
}