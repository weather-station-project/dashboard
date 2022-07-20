using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Services;
using WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel;
using WeatherStationProject.Dashboard.Data.Validations;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ambient-temperatures")]
    public class AmbientTemperatureController : ControllerBase
    {
        private readonly IAmbientTemperatureService _ambientTemperatureService;

        public AmbientTemperatureController(IAmbientTemperatureService ambientTemperatureService)
        {
            _ambientTemperatureService = ambientTemperatureService;
        }

        [HttpGet("last")]
        public async Task<ActionResult<AmbientTemperatureDto>> LastMeasurement()
        {
            var last = await _ambientTemperatureService.GetLastTemperature();

            if (null == last) return NotFound();

            return AmbientTemperatureDto.FromEntity(last);
        }
        
        [HttpGet("historical")]
        public async Task<ActionResult<HistoricalDataDto>> HistoricalData(
            [Required] DateTime since,
            [Required] DateTime until,
            [Required] [GroupingRange] string grouping,
            [Required] bool includeSummary,
            [Required] bool includeMeasurements)
        {
            var records = await _ambientTemperatureService.GetAmbientTemperaturesBetweenDates(since.ToUniversalTime(),
                until.ToUniversalTime());

            if (records.Count == 0) return NotFound();

            return new HistoricalDataDto(records,
                (GroupingValues)Enum.Parse(typeof(GroupingValues), grouping),
                includeSummary,
                includeMeasurements);
        }
    }
}