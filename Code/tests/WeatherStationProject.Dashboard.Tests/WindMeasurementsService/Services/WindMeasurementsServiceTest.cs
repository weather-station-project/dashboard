using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.WindMeasurementsService;

public class WindMeasurementsServiceTest
{
    [Fact]
    public async Task When_Getting_LastWWindMeasurement_Given_Result_Should_Return_RelatedObject()
    {
        // Arrange
        var measurement = new WindMeasurements {Speed = 5, Direction = "E"};
        var repository = new Mock<IWindMeasurementsRepository>();
        repository.Setup(x => x.GetLastMeasurement()).Returns(Task.FromResult(measurement));
        var service = new Dashboard.WindMeasurementsService.Services.WindMeasurementsService(repository.Object);

        // Act
        var result = await service.GetLastWindMeasurements();

        // Assert
        Assert.Equal(measurement, result);
    }

    [Fact]
    public async Task When_Getting_GustInTime_Given_Result_Should_Return_RelatedObject()
    {
        // Arrange
        var measurement = new WindMeasurements {Speed = 5, Direction = "E"};
        var repository = new Mock<IWindMeasurementsRepository>();
        repository.Setup(x => x.GetGustInTime(It.IsAny<int>())).Returns(Task.FromResult(measurement));
        var service = new Dashboard.WindMeasurementsService.Services.WindMeasurementsService(repository.Object);

        // Act
        var result = await service.GetGustInTime(8);

        // Assert
        Assert.Equal(measurement, result);
    }

    [Fact]
    public async Task When_Getting_WindMeasurementsBetweenDates_Given_Result_Should_Return_RelatedObject()
    {
        // Arrange
        var measurement1 = new WindMeasurements {Speed = 5};
        var measurement2 = new WindMeasurements {Speed = 15};
        var parametersRepository = new Mock<IWindMeasurementsRepository>();
        parametersRepository.Setup(x => x.GetMeasurementsBetweenDates(It.IsAny<DateTime>(),
            It.IsAny<DateTime>())).Returns(Task.FromResult(new List<WindMeasurements> {measurement1, measurement2}));
        var service =
            new Dashboard.WindMeasurementsService.Services.WindMeasurementsService(parametersRepository.Object);

        // Act
        var result = await service.GetWindMeasurementsBetweenDates(DateTime.Now, DateTime.Now);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(measurement1.Speed, result[0].Speed);
    }
}