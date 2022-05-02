using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.Data
{
    [Table("air_measurements")]
    public class AirParameters : Measurement
    {
        [Column("pressure")] public decimal Pressure { get; set; }

        [Column("humidity")] public decimal Humidity { get; set; }
    }
}