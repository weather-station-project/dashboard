using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.GroundTemperatureService.Controllers;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;
using WeatherStationProject.Dashboard.GroundTemperatureService.Services;
using WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.GroundTemperatureService;

public class GroundTemperatureControllerTest
{
    [Fact]
    public async Task When_Getting_LastTemperature_Given_Null_Should_Return_Not_Found_Response()
    {
        // Arrange
        var parametersService = new Mock<IGroundTemperatureService>();
        parametersService.Setup(x => x.GetLastTemperature()).Returns(Task.FromResult<GroundTemperature>(null!));
        var controller = new GroundTemperatureController(parametersService.Object);

        // Act
        var response = await controller.LastMeasurement();

        // Assert
        Assert.IsType(new NotFoundResult().GetType(), response.Result);
    }

    [Fact]
    public async Task When_Getting_LastTemperature_Given_Result_Should_Return_RelatedDto()
    {
        // Arrange
        var measurement = new GroundTemperature {Temperature = 5};
        var service = new Mock<IGroundTemperatureService>();
        service.Setup(x => x.GetLastTemperature()).Returns(Task.FromResult(measurement));
        var controller = new GroundTemperatureController(service.Object);

        // Act
        var response = await controller.LastMeasurement();

        // Assert
        Assert.IsType(new GroundTemperatureDto().GetType(), response.Value);
        if (response.Value != null)
            Assert.Equal(GroundTemperatureDto.FromEntity(measurement).Temperature, response.Value.Temperature);
    }

    [Fact]
    public async Task When_Getting_HistoricalData_Given_Empty_Should_Return_Not_Found_Response()
    {
        // Arrange
        var parametersService = new Mock<IGroundTemperatureService>();
        parametersService.Setup(x => x.GetGroundTemperaturesBetweenDates(It.IsAny<DateTime>(),
            It.IsAny<DateTime>())).Returns(Task.FromResult(new List<GroundTemperature>()));
        var controller = new GroundTemperatureController(parametersService.Object);

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
        var measurement = new GroundTemperature
        {
            Temperature = 25,
            DateTime = DateTime.UtcNow
        };
        var parametersService = new Mock<IGroundTemperatureService>();
        parametersService.Setup(x => x.GetGroundTemperaturesBetweenDates(It.IsAny<DateTime>(),
            It.IsAny<DateTime>())).Returns(Task.FromResult(new List<GroundTemperature> {measurement}));
        var controller = new GroundTemperatureController(parametersService.Object);

        // Act
        var response = await controller.HistoricalData(DateTime.Now,
            DateTime.Now, GroupingValues.Days.ToString(), false, false);

        // Assert
        Assert.IsType(
            new HistoricalDataDto(new List<GroundTemperature> {measurement}, GroupingValues.Days, false, false)
                .GetType(), response.Value);
    }
}