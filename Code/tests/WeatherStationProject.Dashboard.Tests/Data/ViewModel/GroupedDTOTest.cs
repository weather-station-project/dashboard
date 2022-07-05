using System;
using WeatherStationProject.Dashboard.Data.Validations;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.Data
{
    public class GroupedDTOTest
    {
        [Theory]
        [InlineData(GroupingValues.Hours)]
        [InlineData(GroupingValues.Days)]
        [InlineData(GroupingValues.Months)]
        public void When_GroupingByKey_Should_Return_ExpectedResult(GroupingValues groupingValue)
        {
            // Arrange
            var dto = new GroupedDTOMock();
            var entity = new MeasurementMock(){Id = 1, DateTime = DateTime.Now};
            
            // Act & Assert
            string expectedKey = groupingValue switch
            {
                GroupingValues.Hours => $"{entity.DateTime:yyyy-MM-dd/HH}",
                GroupingValues.Days => $"{entity.DateTime:yyyy-MM-dd}",
                GroupingValues.Months => $"{entity.DateTime:MMMM}-{entity.DateTime.Year}",
                _ => string.Empty
            };
            Assert.Equal(expectedKey, dto.GetGroupingKeyPublic(entity, groupingValue));
        }
    }
}