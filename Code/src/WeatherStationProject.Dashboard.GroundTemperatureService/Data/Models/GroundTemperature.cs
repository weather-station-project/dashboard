using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Data
{
    [Table("ground_temperatures")]
    public class GroundTemperature : Measurement
    {
        [Column("temperature")] public decimal Temperature { get; set; }
    }
}