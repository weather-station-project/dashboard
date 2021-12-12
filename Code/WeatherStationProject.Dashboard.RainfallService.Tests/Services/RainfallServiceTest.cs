using System;
using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.RainfallService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Tests
{
    public class RainfallServiceTest
    {
        [Fact]
        public async void When_Getting_RainfallDuringTime_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            decimal measurement = 1;
            var repository = new Mock<IRainfallRepository>();
            repository.Setup(x => x.GetRainfallDuringTime(It.IsAny<DateTime>(),
                It.IsAny<DateTime>())).Returns(Task.FromResult(measurement));
            var service = new RainfallService.Services.RainfallService(repository.Object);

            // Act
            var result = await service.GetRainfallDuringTime(DateTime.Now, DateTime.Now);

            // Assert
            Assert.Equal(measurement, result);
        }
    }
}