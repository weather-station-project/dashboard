using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.RainfallService.ViewModel
{
    public class RainfallSummaryDto : SummaryDto
    {
        public decimal MinAmount { get; set; }
        public decimal AvgAmount { get; set; }
        public decimal MaxAmount { get; set; }
    }
}