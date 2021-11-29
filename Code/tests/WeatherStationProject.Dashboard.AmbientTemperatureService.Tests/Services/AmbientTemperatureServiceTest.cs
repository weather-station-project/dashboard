using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.AirParametersService.Tests
{
    public class AmbientTemperatureServiceTest
    {
        [Fact]
        public async void When_Getting_LastTemperature_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement = new AmbientTemperature {Temperature = 1};
            var parametersService = new Mock<IRepository<AmbientTemperature>>();
            parametersService.Setup(x => x.GetLastMeasurement()).Returns(Task.FromResult(measurement));
            var service = new AmbientTemperatureService.Services.AmbientTemperatureService(parametersService.Object);

            // Act
            var result = await service.GetLastTemperature();

            // Assert
            Assert.Equal(measurement, result);
        }
    }
}