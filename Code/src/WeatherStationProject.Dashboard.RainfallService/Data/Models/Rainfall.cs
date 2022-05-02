using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.RainfallService.Data
{
    [Table("rainfall")]
    public class Rainfall : Measurement
    {
        [Column("amount")] public decimal Amount { get; set; }
    }
}