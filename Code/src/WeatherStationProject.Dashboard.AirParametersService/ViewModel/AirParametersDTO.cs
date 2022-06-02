using System;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.AirParametersService.ViewModel
{
    public class AirParametersDTO : SimpleDTO
    {
        public decimal Pressure { get; set; }

        public decimal Humidity { get; set; }

        public static AirParametersDTO FromEntity(AirParameters entity)
        {
            return new AirParametersDTO
            {
                DateTime = entity.DateTime.ToLocalTime(),
                Pressure = entity.Pressure,
                Humidity = entity.Humidity
            };
        }
    }
}