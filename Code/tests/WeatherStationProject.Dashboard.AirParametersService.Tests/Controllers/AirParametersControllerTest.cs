using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.AirParametersService.Controllers;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.AirParametersService.Services;
using WeatherStationProject.Dashboard.AirParametersService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.AirParametersService.Tests
{
    public class AirParametersControllerTest
    {
        [Fact]
        public async Task When_Getting_LastMeasurement_Given_Null_Should_Return_Not_Found_Response()
        {
            // Arrange
            var parametersService = new Mock<IAirParametersService>();
            parametersService.Setup(x => x.GetLastAirParameters()).Returns(Task.FromResult<AirParameters>(null!));
            var controller = new AirParametersController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new NotFoundResult().GetType(), response.Result);
        }

        [Fact]
        public async Task When_Getting_LastMeasurement_Given_Result_Should_Return_RelatedDto()
        {
            // Arrange
            var measurement = new AirParameters {Humidity = 5};
            var parametersService = new Mock<IAirParametersService>();
            parametersService.Setup(x => x.GetLastAirParameters()).Returns(Task.FromResult(measurement));
            var controller = new AirParametersController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new AirParametersDTO().GetType(), response.Value);
            Assert.Equal(AirParametersDTO.FromEntity(measurement).Humidity, response.Value.Humidity);
        }
    }
}