using System;

namespace WeatherStationProject.Dashboard.Data
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }
    }
}
