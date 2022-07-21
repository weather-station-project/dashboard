using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.AmbientTemperatureService
{
    public class AmbientTemperatureServiceTest
    {
        [Fact]
        public async Task When_Getting_LastTemperature_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement = new AmbientTemperature {Temperature = 1};
            var repository = new Mock<IRepository<AmbientTemperature>>();
            repository.Setup(x => x.GetLastMeasurement()).Returns(Task.FromResult(measurement));
            var service = new Dashboard.AmbientTemperatureService.Services.AmbientTemperatureService(repository.Object);

            // Act
            var result = await service.GetLastTemperature();

            // Assert
            Assert.Equal(measurement, result);
        }
        
        [Fact]
        public async Task When_Getting_AmbientTemperaturesBetweenDates_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement1 = new AmbientTemperature {Temperature = 5};
            var measurement2 = new AmbientTemperature {Temperature = 15};
            var parametersRepository = new Mock<IRepository<AmbientTemperature>>();
            parametersRepository.Setup(x => x.GetMeasurementsBetweenDates(It.IsAny<DateTime>(), 
                It.IsAny<DateTime>())).Returns(Task.FromResult(new List<AmbientTemperature>() {measurement1, measurement2}));
            var service = new Dashboard.AmbientTemperatureService.Services.AmbientTemperatureService(parametersRepository.Object);

            // Act
            var result = await service.GetAmbientTemperaturesBetweenDates(DateTime.Now, DateTime.Now);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(measurement1.Temperature, result[0].Temperature);
        }
    }
}