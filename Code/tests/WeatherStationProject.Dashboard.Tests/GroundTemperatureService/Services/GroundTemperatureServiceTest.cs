using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.Data;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.GroundTemperatureService
{
    public class GroundTemperatureServiceTest
    {
        [Fact]
        public async Task When_Getting_LastTemperature_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement = new GroundTemperature {Temperature = 1};
            var repository = new Mock<IRepository<GroundTemperature>>();
            repository.Setup(x => x.GetLastMeasurement()).Returns(Task.FromResult(measurement));
            var service = new Dashboard.GroundTemperatureService.Services.GroundTemperatureService(repository.Object);

            // Act
            var result = await service.GetLastTemperature();

            // Assert
            Assert.Equal(measurement, result);
        }
        
        [Fact]
        public async Task When_Getting_GroundTemperaturesBetweenDates_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement1 = new GroundTemperature {Temperature = 5};
            var measurement2 = new GroundTemperature {Temperature = 15};
            var parametersRepository = new Mock<IRepository<GroundTemperature>>();
            parametersRepository.Setup(x => x.GetMeasurementsBetweenDates(It.IsAny<DateTime>(), 
                It.IsAny<DateTime>())).Returns(Task.FromResult(new List<GroundTemperature>() {measurement1, measurement2}));
            var service = new Dashboard.GroundTemperatureService.Services.GroundTemperatureService(parametersRepository.Object);

            // Act
            var result = await service.GetGroundTemperaturesBetweenDates(DateTime.Now, DateTime.Now);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(measurement1.Temperature, result[0].Temperature);
        }
    }
}