using WeatherStationProject.Dashboard.Data.ViewModel;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel
{
    public class GroundTemperatureDto : MeasurementDto
    {
        public decimal Temperature { get; set; }

        public static GroundTemperatureDto FromEntity(GroundTemperature entity)
        {
            return new GroundTemperatureDto
            {
                Id = entity.Id,
                DateTime = entity.DateTime.ToLocalTime(),
                Temperature = entity.Temperature
            };
        }
    }
}