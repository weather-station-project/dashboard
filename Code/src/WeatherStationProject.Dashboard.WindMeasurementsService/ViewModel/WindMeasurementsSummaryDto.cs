using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel
{
    public class WindMeasurementsSummaryDto : SummaryDto
    {
        public decimal AvgSpeed { get; set; }
        
        public decimal MaxGust { get; set; }

        public string PredominantDirection { get; set; }
    }
}