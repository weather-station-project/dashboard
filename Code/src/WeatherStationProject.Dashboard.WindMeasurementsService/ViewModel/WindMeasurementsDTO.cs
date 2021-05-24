using System;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel
{
    public class WindMeasurementsDTO
    {
        public DateTime DateTime { get; set; }

        public int Speed { get; set; }

        public string Direction { get; set; }

        public static WindMeasurementsDTO FromEntity(WindMeasurements entity)
        {
            return new()
            {
                DateTime = entity.DateTime,
                Speed = entity.Speed,
                Direction = entity.Direction
            };
        }
    }
}
