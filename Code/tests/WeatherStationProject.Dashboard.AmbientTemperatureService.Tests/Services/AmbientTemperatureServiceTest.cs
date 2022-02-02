using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Tests
{
    public class AmbientTemperatureServiceTest
    {
        [Fact]
        public async Task When_Getting_LastTemperature_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement = new AmbientTemperature {Temperature = 1};
            var repository = new Mock<IRepository<AmbientTemperature>>();
            repository.Setup(x => x.GetLastMeasurement()).Returns(Task.FromResult(measurement));
            var service = new Services.AmbientTemperatureService(repository.Object);

            // Act
            var result = await service.GetLastTemperature();

            // Assert
            Assert.Equal(measurement, result);
        }
    }
}