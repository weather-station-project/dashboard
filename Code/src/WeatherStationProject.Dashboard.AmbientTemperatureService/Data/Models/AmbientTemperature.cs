using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Data
{
    [Table("ambient_temperatures")]
    public class AmbientTemperature : Measurement
    {
        [Column("temperature")] public decimal Temperature { get; set; }
    }
}