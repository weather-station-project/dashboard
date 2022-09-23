using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.Data;

public class RepositoryTest
{
    [Fact]
    public async Task When_Getting_LastMeasurement_Should_Return_ExpectedData()
    {
        // Arrange
        var m1 = new MeasurementMock {Id = 1, DateTime = DateTime.Now.AddDays(-1)};
        var m2 = new MeasurementMock {Id = 2, DateTime = DateTime.Now};
        var mockDbSet = new List<MeasurementMock> {m1, m2}.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(x => x.Set<MeasurementMock>()).Returns(mockDbSet.Object);

        // Act & Assert
        var repository = new RepositoryMock(mockDbContext.Object);
        Assert.Equal(m2, await repository.GetLastMeasurement());
    }

    [Fact]
    public async Task When_Getting_MeasurementsBetweenDates_Should_Return_ExpectedData()
    {
        // Arrange
        var m1 = new MeasurementMock {Id = 1, DateTime = DateTime.Now.AddDays(-1)};
        var m2 = new MeasurementMock {Id = 2, DateTime = DateTime.Now};
        var m3 = new MeasurementMock {Id = 2, DateTime = DateTime.Now.AddDays(-10)};
        var mockDbSet = new List<MeasurementMock> {m1, m2, m3}.AsQueryable().BuildMockDbSet();

        var mockDbContext = new Mock<DbContext>();
        mockDbContext.Setup(x => x.Set<MeasurementMock>()).Returns(mockDbSet.Object);

        // Act
        var repository = new RepositoryMock(mockDbContext.Object);
        var results = await repository.GetMeasurementsBetweenDates(DateTime.Now.AddDays(-2), DateTime.Now);

        // Assert
        Assert.Equal(2, results.Count);
        Assert.Equal(m2.Id, results[0].Id);
        Assert.Equal(m1.Id, results[1].Id);
    }
}