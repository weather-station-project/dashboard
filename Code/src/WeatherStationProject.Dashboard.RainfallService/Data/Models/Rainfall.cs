using System.ComponentModel.DataAnnotations.Schema;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.RainfallService.Data
{
    [Table(name: "rainfall")]
    public class Rainfall : Measurement
    {
        [Column(name: "amount")]
        public int Amount { get; set; }
    }
}
