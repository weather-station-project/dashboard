using System;
using WeatherStationProject.Dashboard.AirParametersService.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.ViewModel
{
    public class AirParametersDTO
    {
        public DateTime DateTime { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public static AirParametersDTO FromEntity(AirParameters entity)
        {
            return new()
            {
                DateTime = entity.DateTime,
                Pressure = entity.Pressure,
                Humidity = entity.Humidity
            };
        }
    }
}
