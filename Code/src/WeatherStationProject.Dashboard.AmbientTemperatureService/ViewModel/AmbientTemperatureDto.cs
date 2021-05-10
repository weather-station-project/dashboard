﻿using System;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel
{
    public class AmbientTemperatureDTO
    {
        public DateTime DateTime { get; set; }

        public int Temperature { get; set; }

        public static AmbientTemperatureDTO FromEntity(AmbientTemperature entity)
        {
            return new()
            {
                DateTime = entity.DateTime,
                Temperature = entity.Temperature
            };
        }
    }
}
