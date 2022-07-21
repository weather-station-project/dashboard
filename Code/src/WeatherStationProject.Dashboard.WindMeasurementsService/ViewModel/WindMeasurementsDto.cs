using System;
using WeatherStationProject.Dashboard.Data.ViewModel;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel
{
    public class WindMeasurementsDto : MeasurementDto
    {
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