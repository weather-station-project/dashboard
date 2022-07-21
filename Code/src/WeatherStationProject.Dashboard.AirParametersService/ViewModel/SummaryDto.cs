namespace WeatherStationProject.Dashboard.AirParametersService.ViewModel
{
    public class SummaryDto
    {
        public decimal MaxPressure { get; set; }
        public decimal AvgPressure { get; set; }
        public decimal MinPressure { get; set; }
        
        public decimal MaxHumidity { get; set; }
        public decimal AvgHumidity { get; set; }
        public decimal MinHumidity { get; set; }
    }
}