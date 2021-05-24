using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.Data
{
    [Table(name: "air_measurements")]
    public class AirParameters : Measurement
    {
        [Column(name: "pressure")]
        public decimal Pressure { get; set; }

        [Column(name: "humidity")]
        public decimal Humidity { get; set; }
    }
}
