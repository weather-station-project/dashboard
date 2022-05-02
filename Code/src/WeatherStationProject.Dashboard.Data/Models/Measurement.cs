using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherStationProject.Dashboard.Data
{
    public abstract class Measurement
    {
        [Key] [Column("id")] public int Id { get; set; }

        [Column("date_time")] public DateTime DateTime { get; set; }
    }
}