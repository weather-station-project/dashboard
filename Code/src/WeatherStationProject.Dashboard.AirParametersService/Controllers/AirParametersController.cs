using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.AirParametersService.Services;
using WeatherStationProject.Dashboard.AirParametersService.ViewModel;
using WeatherStationProject.Dashboard.Data.Validations;

namespace WeatherStationProject.Dashboard.AirParametersService.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/air-parameters")]
    public class AirParametersController : ControllerBase
    {
        private readonly IAirParametersService _airParametersService;

        public AirParametersController(IAirParametersService airParametersService)
        {
            _airParametersService = airParametersService;
        }

        [HttpGet("last")]
        public async Task<ActionResult<AirParametersDTO>> LastMeasurement()
        {
            var last = await _airParametersService.GetLastAirParameters();

            if (null == last) return NotFound();

            return AirParametersDTO.FromEntity(last);
        }

        [HttpGet("historical")]
        public async Task<ActionResult<HistoricalDataDTO>> HistoricalData(
            [Required] DateTime since,
            [Required] DateTime until,
            [Required] [GroupingRange] string grouping,
            [Required] bool includeSummary,
            [Required] bool includeMeasurements)
        {
            var records = await _airParametersService.GetAirParametersBetweenDates(since.ToUniversalTime(),
                until.ToUniversalTime());

            if (records.Count == 0) return NotFound();

            return new HistoricalDataDTO(records,
                (GroupingValues)Enum.Parse(typeof(GroupingValues), grouping),
                includeSummary,
                includeMeasurements);
        }
    }
}