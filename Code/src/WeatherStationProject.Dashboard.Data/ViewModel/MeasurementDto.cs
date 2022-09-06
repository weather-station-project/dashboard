using System;

namespace WeatherStationProject.Dashboard.Data.ViewModel
{
    public abstract class MeasurementDto
    {
        public int Id { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}