using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Data
{
    [Table("wind_measurements")]
    public class WindMeasurements : Measurement
    {
        [Column("speed")] public decimal Speed { get; set; }

        [Column("direction")] public string Direction { get; set; }
    }
}