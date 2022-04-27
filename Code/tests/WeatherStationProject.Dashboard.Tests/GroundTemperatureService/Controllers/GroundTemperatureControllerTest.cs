using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;
using WeatherStationProject.Dashboard.GroundTemperatureService.Services;
using WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.GroundTemperatureService
{
    public class GroundTemperatureControllerTest
    {
        [Fact]
        public async Task When_Getting_LastTemperature_Given_Null_Should_Return_Not_Found_Response()
        {
            // Arrange
            var parametersService = new Mock<IGroundTemperatureService>();
            parametersService.Setup(x => x.GetLastTemperature()).Returns(Task.FromResult<GroundTemperature>(null!));
            var controller = new GroundTemperatureController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new NotFoundResult().GetType(), response.Result);
        }

        [Fact]
        public async Task When_Getting_LastTemperature_Given_Result_Should_Return_RelatedDto()
        {
            // Arrange
            var measurement = new GroundTemperature {Temperature = 5};
            var service = new Mock<IGroundTemperatureService>();
            service.Setup(x => x.GetLastTemperature()).Returns(Task.FromResult(measurement));
            var controller = new GroundTemperatureController(service.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new GroundTemperatureDTO().GetType(), response.Value);
            if (response.Value != null)
                Assert.Equal(GroundTemperatureDTO.FromEntity(measurement).Temperature, response.Value.Temperature);
        }
    }
}