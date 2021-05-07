using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Services;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AmbientTemperaturesController : ControllerBase
    {
        private readonly IAmbientTemperatureService _ambientTemperatureService;

        public AmbientTemperaturesController(IAmbientTemperatureService ambientTemperatureService)
        {
            _ambientTemperatureService = ambientTemperatureService;
        }

        [HttpGet]
        public async Task<List<AmbientTemperature>> LastMeasurement()
        {
            return await _ambientTemperatureService.GetAmbientTemperaturesBetweenDatesAsync(since: DateTime.MinValue, until: DateTime.Now);
        }

        /*[HttpGet]
        public IEnumerable<WeatherForecast> LastMeasurement(DateTime since, DateTime until)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }*/
    }
}
