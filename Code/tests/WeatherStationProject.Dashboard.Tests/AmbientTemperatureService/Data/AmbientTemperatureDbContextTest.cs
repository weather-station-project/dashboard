using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AmbientTemperatureService;

public class AmbientTemperatureDbContextTest
{
    [Fact]
    public void When_Building_Context_Should_Have_Expected_Table()
    {
        // Act & Assert
        var context = new AmbientTemperatureDbContext();
        Assert.NotNull(context.AmbientTemperatures);
    }
}