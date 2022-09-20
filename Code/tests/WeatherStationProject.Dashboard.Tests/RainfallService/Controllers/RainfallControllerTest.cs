using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.RainfallService.Controllers;
using WeatherStationProject.Dashboard.RainfallService.Data;
using WeatherStationProject.Dashboard.RainfallService.Services;
using WeatherStationProject.Dashboard.RainfallService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.RainfallService;

public class RainfallControllerTest
{
    [Fact]
    public async Task When_Getting_AmountDuringTime_Given_Result_Should_Return_RelatedDto()
    {
        // Arrange
        decimal measurement = 7;
        var service = new Mock<IRainfallService>();
        service.Setup(x => x.GetRainfallDuringTime(It.IsAny<DateTime>(),
            It.IsAny<DateTime>())).Returns(Task.FromResult(measurement));
        var controller = new RainfallController(service.Object);

        // Act
        var response = await controller.RainfallDuringTime(4);

        // Assert
        Assert.IsType(new RainfallDto().GetType(), response.Value);
        if (response.Value != null)
            Assert.Equal(RainfallDto.FromEntity(measurement, DateTime.Now, DateTime.Now).Amount,
                response.Value.Amount);
    }

    [Fact]
    public async Task When_Getting_HistoricalData_Given_Empty_Should_Return_Not_Found_Response()
    {
        // Arrange
        var parametersService = new Mock<IRainfallService>();
        parametersService.Setup(x => x.GetRainfallMeasurementsBetweenDates(It.IsAny<DateTime>(),
            It.IsAny<DateTime>())).Returns(Task.FromResult(new List<Rainfall>()));
        var controller = new RainfallController(parametersService.Object);

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
        var measurement = new Rainfall
        {
            Amount = 25,
            DateTime = DateTime.UtcNow
        };
        var parametersService = new Mock<IRainfallService>();
        parametersService.Setup(x => x.GetRainfallMeasurementsBetweenDates(It.IsAny<DateTime>(),
            It.IsAny<DateTime>())).Returns(Task.FromResult(new List<Rainfall> {measurement}));
        var controller = new RainfallController(parametersService.Object);

        // Act
        var response = await controller.HistoricalData(DateTime.Now,
            DateTime.Now, GroupingValues.Days.ToString(), false, false);

        // Assert
        Assert.IsType(
            new HistoricalDataDto(new List<Rainfall> {measurement}, GroupingValues.Days, false, false).GetType(),
            response.Value);
    }
}