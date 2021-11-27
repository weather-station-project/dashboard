using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.AirParametersService.Tests
{
    public class AirParametersServiceTest
    {
        [Fact]
        public async void When_Getting_LastAirParameters_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement = new AirParameters {Humidity = 5};
            var parametersService = new Mock<IRepository<AirParameters>>();
            parametersService.Setup(x => x.GetLastMeasurement()).Returns(Task.FromResult(measurement));
            var service = new Services.AirParametersService(parametersService.Object);

            // Act
            var result = await service.GetLastAirParameters();

            // Assert
            Assert.Equal(measurement, result);
        }
    }
}