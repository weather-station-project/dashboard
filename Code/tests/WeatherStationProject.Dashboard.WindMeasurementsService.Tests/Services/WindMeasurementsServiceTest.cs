using System.Threading.Tasks;
using Moq;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;
using Xunit;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Tests
{
    public class WindMeasurementsServiceTest
    {
        [Fact]
        public async Task When_Getting_LastWWindMeasurement_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement = new WindMeasurements {Speed = 5, Direction = "E"};
            var repository = new Mock<IWindMeasurementsRepository>();
            repository.Setup(x => x.GetLastMeasurement()).Returns(Task.FromResult(measurement));
            var service = new Services.WindMeasurementsService(repository.Object);

            // Act
            var result = await service.GetLastWindMeasurements();

            // Assert
            Assert.Equal(measurement, result);
        }
        
        [Fact]
        public async Task When_Getting_GustInTime_Given_Result_Should_Return_RelatedObject()
        {
            // Arrange
            var measurement = new WindMeasurements {Speed = 5, Direction = "E"};
            var repository = new Mock<IWindMeasurementsRepository>();
            repository.Setup(x => x.GetGustInTime(It.IsAny<int>())).Returns(Task.FromResult(measurement));
            var service = new Services.WindMeasurementsService(repository.Object);

            // Act
            var result = await service.GetGustInTime(8);

            // Assert
            Assert.Equal(measurement, result);
        }
    }
}