using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MockQueryable.Moq;
using Moq;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.WindMeasurementsService;

public class WindMeasurementsRepositoryTest
{
    [Fact]
    public async Task When_Getting_RainfallDuringTime_Should_Return_ExpectedData()
    {
        // Arrange
        var mockDbSet = new List<WindMeasurements>
        {
            new() {Speed = 10, Direction = "NO", DateTime = DateTime.Now.AddHours(-1)},
            new() {Speed = 50, Direction = "E", DateTime = DateTime.Now.AddHours(-2)},
            new() {Speed = 100, DateTime = DateTime.Now.AddYears(-1)}, // This one is out
            new() {Speed = 100, DateTime = DateTime.Now.AddYears(-1)} // This one is out
        }.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<WindMeasurementsDbContext>();
        mockDbContext.Setup(x => x.WindMeasurements).Returns(mockDbSet.Object);

        // Act
        var repository = new WindMeasurementsRepository(mockDbContext.Object);
        var result = await repository.GetGustInTime(180);

        // Assert
        Assert.Equal(50, result.Speed);
        Assert.Equal("E", result.Direction);
    }
}