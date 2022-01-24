using WeatherStationProject.Dashboard.WindMeasurementsService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Tests
{
    public class WindMeasurementsDbContextTest
    {
        [Fact]
        public void When_Building_Context_Should_Have_Expected_Table()
        {
            // Act & Assert
            var context = new WindMeasurementsDbContext();
            Assert.NotNull(context.WindMeasurements);
        }
    }
}