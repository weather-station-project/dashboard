using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.WindMeasurementsService.Controllers;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;
using WeatherStationProject.Dashboard.WindMeasurementsService.Services;
using WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.WindMeasurementsService
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
            Assert.IsType(new WindMeasurementsDto().GetType(), response.Value);
            Assert.Equal(WindMeasurementsDto.FromEntity(measurement).Direction, response.Value?.Direction);
            if (response.Value != null)
                Assert.Equal(WindMeasurementsDto.FromEntity(measurement).Speed, response.Value.Speed);
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
            Assert.IsType(new WindMeasurementsDto().GetType(), response.Value);
            Assert.Equal(WindMeasurementsDto.FromEntity(measurement).Direction, response.Value?.Direction);
            if (response.Value != null)
                Assert.Equal(WindMeasurementsDto.FromEntity(measurement).Speed, response.Value.Speed);
        }
        
        [Fact]
        public async Task When_Getting_HistoricalData_Given_Empty_Should_Return_Not_Found_Response()
        {
            // Arrange
            var parametersService = new Mock<IWindMeasurementsService>();
            parametersService.Setup(x => x.GetWindMeasurementsBetweenDates(It.IsAny<DateTime>(), 
                It.IsAny<DateTime>())).Returns(Task.FromResult(new List<WindMeasurements>()));
            var controller = new WindMeasurementsController(parametersService.Object);

            // Act
            var response = await controller.HistoricalData(DateTime.Now, 
                DateTime.Now, GroupingValues.Days.ToString(), false, false);

            // Assert
            Assert.IsType(new NotFoundResult().GetType(), response.Result);
        }
        
        [Fact]
        public async Task When_Getting_HistoricalData_Given_Result_Should_Return_Expected_Response()
        {
            // Arrange
            var measurement = new WindMeasurements
            {
                Speed = 2,
                DateTime = DateTime.UtcNow
            };
            var parametersService = new Mock<IWindMeasurementsService>();
            parametersService.Setup(x => x.GetWindMeasurementsBetweenDates(It.IsAny<DateTime>(), 
                It.IsAny<DateTime>())).Returns(Task.FromResult(new List<WindMeasurements> {measurement}));
            var controller = new WindMeasurementsController(parametersService.Object);

            // Act
            var response = await controller.HistoricalData(DateTime.Now, 
                DateTime.Now, GroupingValues.Days.ToString(), false, false);

            // Assert
            Assert.IsType(new HistoricalDataDto(new List<WindMeasurements> {measurement}, GroupingValues.Days, false, false).GetType(), response.Value);
        }
    }
}