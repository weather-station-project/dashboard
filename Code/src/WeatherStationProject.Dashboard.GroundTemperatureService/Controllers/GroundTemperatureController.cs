using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.GroundTemperatureService.Services;
using WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ground-temperatures")]
    public class GroundTemperatureController : ControllerBase
    {
        private readonly IGroundTemperatureService _groundTemperatureService;

        public GroundTemperatureController(IGroundTemperatureService groundTemperatureService)
        {
            _groundTemperatureService = groundTemperatureService;
        }

        [HttpGet("last")]
        public async Task<ActionResult<GroundTemperatureDto>> LastMeasurement()
        {
            var last = await _groundTemperatureService.GetLastTemperature();

            if (null == last) return NotFound();

            return GroundTemperatureDto.FromEntity(last);
        }
        
        [HttpGet("historical")]
        public async Task<ActionResult<HistoricalDataDto>> HistoricalData(
            [Required] DateTime since,
            [Required] DateTime until,
            [Required] [GroupingRange] string grouping,
            [Required] bool includeSummary,
            [Required] bool includeMeasurements)
        {
            var records = await _groundTemperatureService.GetGroundTemperaturesBetweenDates(since.ToUniversalTime(),
                until.ToUniversalTime());

            if (records.Count == 0) return NotFound();

            return new HistoricalDataDto(records,
                (GroupingValues)Enum.Parse(typeof(GroupingValues), grouping),
                includeSummary,
                includeMeasurements);
        }
    }
}