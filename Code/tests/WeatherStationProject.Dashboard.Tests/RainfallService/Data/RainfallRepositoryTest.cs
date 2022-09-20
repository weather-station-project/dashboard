using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MockQueryable.Moq;
using Moq;
using WeatherStationProject.Dashboard.RainfallService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.RainfallService;

public class RainfallRepositoryTest
{
    [Fact]
    public async Task When_Getting_RainfallDuringTime_Should_Return_ExpectedData()
    {
        // Arrange
        var mockDbSet = new List<Rainfall>
        {
            new() {Amount = 10, DateTime = DateTime.Now.AddHours(-1)},
            new() {Amount = 10, DateTime = DateTime.Now.AddHours(-2)},
            new() {Amount = 100, DateTime = DateTime.Now.AddYears(-1)}, // This one is out
            new() {Amount = 100, DateTime = DateTime.Now.AddYears(-1)} // This one is out
        }.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<RainfallDbContext>();
        mockDbContext.Setup(x => x.Rainfall).Returns(mockDbSet.Object);

        // Act & Assert
        var repository = new RainfallRepository(mockDbContext.Object);
        Assert.Equal(20, await repository.GetRainfallDuringTime(DateTime.Now.AddHours(-3),
            DateTime.Now));
    }
}