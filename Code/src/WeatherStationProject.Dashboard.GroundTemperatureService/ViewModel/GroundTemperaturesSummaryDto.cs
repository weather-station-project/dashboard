using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel
{
    public class GroundTemperaturesSummaryDto : SummaryDto
    {
        public decimal MaxTemperature { get; set; }
        public decimal AvgTemperature { get; set; }
        public decimal MinTemperature { get; set; }
    }
}