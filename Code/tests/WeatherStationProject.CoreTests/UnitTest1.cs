using System;
using WeatherStationProject.Core;
using Xunit;

namespace WeatherStationProject.CoreTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var a = new Class1();
            Assert.Equal(expected: 1, actual: a.GetSomething());
        }

        [Fact]
        public void Test2()
        {
            var a = new Class1();
            Assert.Equal(expected: 1, actual: a.GetSomething());
        }
    }
}
