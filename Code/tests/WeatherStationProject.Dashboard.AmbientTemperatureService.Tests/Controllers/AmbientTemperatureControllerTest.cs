using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Services;
using WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Tests
{
    public class AmbientTemperatureControllerTest
    {
        [Fact]
        public async Task When_Getting_LastTemperature_Given_Null_Should_Return_Not_Found_Response()
        {
            // Arrange
            var parametersService = new Mock<IAmbientTemperatureService>();
            parametersService.Setup(x => x.GetLastTemperature()).Returns(Task.FromResult<AmbientTemperature>(null!));
            var controller = new AmbientTemperatureController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new NotFoundResult().GetType(), response.Result);
        }

        [Fact]
        public async Task When_Getting_LastMeasurement_Given_Result_Should_Return_RelatedDto()
        {
            // Arrange
            var measurement = new AmbientTemperature {Temperature = 5};
            var parametersService = new Mock<IAmbientTemperatureService>();
            parametersService.Setup(x => x.GetLastTemperature()).Returns(Task.FromResult(measurement));
            var controller = new AmbientTemperatureController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new AmbientTemperatureDTO().GetType(), response.Value);
            if (response.Value != null)
                Assert.Equal(AmbientTemperatureDTO.FromEntity(measurement).Temperature, response.Value.Temperature);
        }
    }
}