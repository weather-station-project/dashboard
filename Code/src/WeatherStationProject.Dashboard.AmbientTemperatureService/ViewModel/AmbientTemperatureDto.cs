using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel
{
    public class AmbientTemperatureDto : MeasurementDto
    {
        public decimal Temperature { get; set; }

        public static AmbientTemperatureDto FromEntity(AmbientTemperature entity)
        {
            return new AmbientTemperatureDto
            {
                Id = entity.Id,
                DateTime = entity.DateTime.ToLocalTime(),
                Temperature = entity.Temperature
            };
        }
    }
}