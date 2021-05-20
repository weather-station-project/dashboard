using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Data
{
    [Table(name: "ground_temperatures")]
    public class GroundTemperature : Measurement
    {
        [Column(name: "temperature")]
        public int Temperature { get; set; }
    }
}
