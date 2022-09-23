using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Services;
using WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel;
using WeatherStationProject.Dashboard.Data.Validations;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AmbientTemperatureService;

public class AmbientTemperatureControllerTest
{
    [Fact]
    public async Task When_Getting_LastTemperature_Given_Null_Should_Return_Not_Found_Response()
    {
        // Arrange
        var parametersService = new Mock<IAmbientTemperatureService>();
        parametersService.Setup(x => x.GetLastTemperature()).Returns(Task.FromResult<AmbientTemperature>(null!));
        var controller = new AmbientTemperatureController(parametersService.Object);

        // Act
        var response = await controller.LastMeasurement();

        // Assert
        Assert.IsType(new NotFoundResult().GetType(), response.Result);
    }

    [Fact]
    public async Task When_Getting_LastMeasurement_Given_Result_Should_Return_RelatedDto()
    {
        // Arrange
        var measurement = new AmbientTemperature {Temperature = 5};
        var parametersService = new Mock<IAmbientTemperatureService>();
        parametersService.Setup(x => x.GetLastTemperature()).Returns(Task.FromResult(measurement));
        var controller = new AmbientTemperatureController(parametersService.Object);

        // Act
        var response = await controller.LastMeasurement();

        // Assert
        Assert.IsType(new AmbientTemperatureDto().GetType(), response.Value);
        if (response.Value != null)
            Assert.Equal(AmbientTemperatureDto.FromEntity(measurement).Temperature, response.Value.Temperature);
    }

    [Fact]
    public async Task When_Getting_HistoricalData_Given_Empty_Should_Return_Not_Found_Response()
    {
        // Arrange
        var parametersService = new Mock<IAmbientTemperatureService>();
        parametersService.Setup(x => x.GetAmbientTemperaturesBetweenDates(It.IsAny<DateTime>(),
            It.IsAny<DateTime>())).Returns(Task.FromResult(new List<AmbientTemperature>()));
        var controller = new AmbientTemperatureController(parametersService.Object);

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
        var measurement = new AmbientTemperature
        {
            Temperature = 80,
            DateTime = DateTime.UtcNow
        };
        var parametersService = new Mock<IAmbientTemperatureService>();
        parametersService.Setup(x => x.GetAmbientTemperaturesBetweenDates(It.IsAny<DateTime>(),
            It.IsAny<DateTime>())).Returns(Task.FromResult(new List<AmbientTemperature> {measurement}));
        var controller = new AmbientTemperatureController(parametersService.Object);

        // Act
        var response = await controller.HistoricalData(DateTime.Now,
            DateTime.Now, GroupingValues.Days.ToString(), false, false);

        // Assert
        Assert.IsType(
            new HistoricalDataDto(new List<AmbientTemperature> {measurement}, GroupingValues.Days, false, false)
                .GetType(), response.Value);
    }
}