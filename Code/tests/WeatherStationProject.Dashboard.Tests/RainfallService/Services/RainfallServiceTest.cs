using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.RainfallService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.RainfallService
{
    public class RainfallServiceTest
    {
        [Fact]
        public async Task When_Getting_RainfallDuringTime_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            decimal measurement = 1;
            var repository = new Mock<IRainfallRepository>();
            repository.Setup(x => x.GetRainfallDuringTime(It.IsAny<DateTime>(),
                It.IsAny<DateTime>())).Returns(Task.FromResult(measurement));
            var service = new Dashboard.RainfallService.Services.RainfallService(repository.Object);

            // Act
            var result = await service.GetRainfallDuringTime(DateTime.Now, DateTime.Now);

            // Assert
            Assert.Equal(measurement, result);
        }
        
        [Fact]
        public async Task When_Getting_RainfallMeasurementsBetweenDates_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement1 = new Rainfall {Amount = 5};
            var measurement2 = new Rainfall {Amount = 15};
            var parametersRepository = new Mock<IRainfallRepository>();
            parametersRepository.Setup(x => x.GetMeasurementsBetweenDates(It.IsAny<DateTime>(), 
                It.IsAny<DateTime>())).Returns(Task.FromResult(new List<Rainfall> {measurement1, measurement2}));
            var service = new Dashboard.RainfallService.Services.RainfallService(parametersRepository.Object);

            // Act
            var result = await service.GetRainfallMeasurementsBetweenDates(DateTime.Now, DateTime.Now);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(measurement1.Amount, result[0].Amount);
        }
    }
}