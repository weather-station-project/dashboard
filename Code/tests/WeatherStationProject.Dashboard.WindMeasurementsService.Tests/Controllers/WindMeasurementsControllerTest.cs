using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.WindMeasurementsService.Controllers;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;
using WeatherStationProject.Dashboard.WindMeasurementsService.Services;
using WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Tests
{
    public class WindMeasurementsControllerTest
    {
        [Fact]
        public async Task When_Getting_LastMeasurement_Given_Null_Should_Return_Not_Found_Response()
        {
            // Arrange
            var parametersService = new Mock<IWindMeasurementsService>();
            parametersService.Setup(x => x.GetLastWindMeasurements()).Returns(Task.FromResult<WindMeasurements>(null!));
            var controller = new WindMeasurementsController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new NotFoundResult().GetType(), response.Result);
        }

        [Fact]
        public async Task When_Getting_LastMeasurement_Given_Result_Should_Return_RelatedDto()
        {
            // Arrange
            var measurement = new WindMeasurements {Speed = 5, Direction = "NO"};
            var parametersService = new Mock<IWindMeasurementsService>();
            parametersService.Setup(x => x.GetLastWindMeasurements()).Returns(Task.FromResult(measurement));
            var controller = new WindMeasurementsController(parametersService.Object);

            // Act
            var response = await controller.LastMeasurement();

            // Assert
            Assert.IsType(new WindMeasurementsDTO().GetType(), response.Value);
            Assert.Equal(WindMeasurementsDTO.FromEntity(measurement).Direction, response.Value?.Direction);
            if (response.Value != null)
                Assert.Equal(WindMeasurementsDTO.FromEntity(measurement).Speed, response.Value.Speed);
        }

        [Fact]
        public async Task When_Getting_GustInTime_Given_Null_Should_Return_Not_Found_Response()
        {
            // Arrange
            var parametersService = new Mock<IWindMeasurementsService>();
            parametersService.Setup(x => x.GetGustInTime(It.IsAny<int>()))
                .Returns(Task.FromResult<WindMeasurements>(null!));
            var controller = new WindMeasurementsController(parametersService.Object);

            // Act
            var response = await controller.GustInTime(5);

            // Assert
            Assert.IsType(new NotFoundResult().GetType(), response.Result);
        }

        [Fact]
        public async Task When_Getting_GustInTime_Given_Result_Should_Return_RelatedDto()
        {
            // Arrange
            var measurement = new WindMeasurements {Speed = 45, Direction = "NO-W"};
            var parametersService = new Mock<IWindMeasurementsService>();
            parametersService.Setup(x => x.GetGustInTime(It.IsAny<int>())).Returns(Task.FromResult(measurement));
            var controller = new WindMeasurementsController(parametersService.Object);

            // Act
            var response = await controller.GustInTime(99);

            // Assert
            Assert.IsType(new WindMeasurementsDTO().GetType(), response.Value);
            Assert.Equal(WindMeasurementsDTO.FromEntity(measurement).Direction, response.Value?.Direction);
            if (response.Value != null)
                Assert.Equal(WindMeasurementsDTO.FromEntity(measurement).Speed, response.Value.Speed);
        }
    }
}