using WeatherStationProject.Dashboard.GroundTemperatureService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.GroundTemperatureService
{
    public class GroundTemperatureDbContextTest
    {
        [Fact]
        public void When_Building_Context_Should_Have_Expected_Table()
        {
            // Act & Assert
            var context = new GroundTemperatureDbContext();
            Assert.NotNull(context.GroundTemperatures);
        }
    }
}