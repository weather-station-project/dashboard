using WeatherStationProject.Dashboard.RainfallService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.RainfallService;

public class RainfallDbContextTest
{
    [Fact]
    public void When_Building_Context_Should_Have_Expected_Table()
    {
        // Act & Assert
        var context = new RainfallDbContext();
        Assert.NotNull(context.Rainfall);
    }
}