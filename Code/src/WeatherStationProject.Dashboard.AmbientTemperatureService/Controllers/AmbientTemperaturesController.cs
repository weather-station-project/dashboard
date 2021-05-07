using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Services;
using WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AmbientTemperaturesController : ControllerBase
    {
        private readonly IAmbientTemperatureService _ambientTemperatureService;
        private readonly IMapper _mapper;

        public AmbientTemperaturesController(IAmbientTemperatureService ambientTemperatureService,
                                             IMapper mapper)
        {
            _ambientTemperatureService = ambientTemperatureService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<AmbientTemperatureDto>> LastMeasurement()
        {
            return (await _ambientTemperatureService.GetAmbientTemperaturesBetweenDatesAsync(since: DateTime.MinValue, until: DateTime.Now))
                .Select(x => _mapper.Map<AmbientTemperatureDto>(x)).ToList();
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
