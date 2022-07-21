namespace WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel
{
    public class SummaryDto
    {
        public decimal AvgSpeed { get; set; }
        
        public decimal MaxGust { get; set; }

        public string PredominantDirection { get; set; }
    }
}