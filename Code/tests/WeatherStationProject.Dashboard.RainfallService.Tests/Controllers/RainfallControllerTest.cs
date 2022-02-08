using System;
using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.RainfallService.Controllers;
using WeatherStationProject.Dashboard.RainfallService.Services;
using WeatherStationProject.Dashboard.RainfallService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.RainfallService.Tests
{
    public class RainfallControllerTest
    {
        [Fact]
        public async Task When_Getting_AmountDuringTime_Given_Result_Should_Return_RelatedDto()
        {
            // Arrange
            decimal measurement = 7;
            var service = new Mock<IRainfallService>();
            service.Setup(x => x.GetRainfallDuringTime(It.IsAny<DateTime>(),
                It.IsAny<DateTime>())).Returns(Task.FromResult(measurement));
            var controller = new RainfallController(service.Object);

            // Act
            var response = await controller.RainfallDuringTime(4);

            // Assert
            Assert.IsType(new RainfallDTO().GetType(), response.Value);
            Assert.Equal(RainfallDTO.FromEntity(measurement, DateTime.Now, DateTime.Now).Amount,
                response.Value.Amount);
        }
    }
}