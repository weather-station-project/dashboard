using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Data
{
    [Table(name: "wind_measurements")]
    public class WindMeasurements : Measurement
    {
        [Column(name: "speed")]
        public int Speed { get; set; }

        [Column(name: "direction")]
        public string Direction { get; set; }
    }
}
