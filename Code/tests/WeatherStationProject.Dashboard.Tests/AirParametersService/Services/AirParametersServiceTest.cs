using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AirParametersService
{
    public class AirParametersServiceTest
    {
        [Fact]
        public async Task When_Getting_LastAirParameters_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement = new AirParameters {Humidity = 5};
            var parametersRepository = new Mock<IRepository<AirParameters>>();
            parametersRepository.Setup(x => x.GetLastMeasurement()).Returns(Task.FromResult(measurement));
            var service = new Dashboard.AirParametersService.Services.AirParametersService(parametersRepository.Object);

            // Act
            var result = await service.GetLastAirParameters();

            // Assert
            Assert.Equal(measurement, result);
        }
    }
}