using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Data
{
    [Table(name: "ambient_temperatures")]
    public class AmbientTemperature : Measurement
    {
        [Column(name: "temperature")]
        public decimal Temperature { get; set; }
    }
}
