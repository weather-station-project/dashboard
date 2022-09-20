using WeatherStationProject.Dashboard.Data.Validations;
using Xunit;

namespace WeatherStationProject.Dashboard.Tests.Data;

public class GroupingRangeAttributeTest
{
    [Theory]
    [InlineData("Hours", true)]
    [InlineData("Days", true)]
    [InlineData("Months", true)]
    [InlineData("test", false)]
    public void When_Checking_IsValid_Should_Return_ExpectedResult(string value, bool expectedResult)
    {
        // Arrange
        var attribute = new GroupingRangeAttribute();

        // Act & Assert
        Assert.Equal(expectedResult, attribute.IsValid(value));
    }
}