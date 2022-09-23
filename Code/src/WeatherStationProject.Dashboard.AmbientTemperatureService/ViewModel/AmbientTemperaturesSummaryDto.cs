using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel
{
    public class AmbientTemperaturesSummaryDto : SummaryDto
    {
        public decimal MaxTemperature { get; set; }
        public decimal AvgTemperature { get; set; }
        public decimal MinTemperature { get; set; }
    }
}