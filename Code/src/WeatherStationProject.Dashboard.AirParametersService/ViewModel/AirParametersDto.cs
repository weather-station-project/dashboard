using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.AirParametersService.ViewModel
{
    public class AirParametersDto : MeasurementDto
    {
        public decimal Pressure { get; set; }

        public decimal Humidity { get; set; }

        public static AirParametersDto FromEntity(AirParameters entity)
        {
            return new AirParametersDto
            {
                DateTime = entity.DateTime.ToLocalTime(),
                Pressure = entity.Pressure,
                Humidity = entity.Humidity
            };
        }
    }
}