using System;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel
{
    public class WindMeasurementsDto
    {
        public DateTime DateTime { get; set; }

        public decimal Speed { get; set; }

        public string Direction { get; set; }

        public static WindMeasurementsDto FromEntity(WindMeasurements entity)
        {
            return new WindMeasurementsDto
            {
                DateTime = entity.DateTime.ToLocalTime(),
                Speed = entity.Speed,
                Direction = entity.Direction
            };
        }
    }
}