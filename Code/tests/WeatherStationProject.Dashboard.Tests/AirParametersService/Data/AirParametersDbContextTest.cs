using WeatherStationProject.Dashboard.AirParametersService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AirParametersService;

public class AirParametersDbContextTest
{
    [Fact]
    public void When_Building_Context_Should_Have_Expected_Table()
    {
        // Act & Assert
        var context = new AirParametersDbContext();
        Assert.NotNull(context.AirParameters);
    }
}