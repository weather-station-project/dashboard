using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.AirParametersService.Services;
using WeatherStationProject.Dashboard.AirParametersService.ViewModel;
using WeatherStationProject.Dashboard.Core.DateTime;
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
        public async Task<ActionResult<AirParametersDto>> LastMeasurement()
        {
            var last = await _airParametersService.GetLastAirParameters();

            if (null == last) return NotFound();

            return AirParametersDto.FromEntity(last);
        }

        [HttpGet("historical")]
        public async Task<ActionResult<HistoricalDataDto>> HistoricalData(
            [Required] DateTime since,
            [Required] DateTime until,
            [Required] [GroupingRange] string grouping,
            [Required] bool includeSummary,
            [Required] bool includeMeasurements)
        {
            var records = await _airParametersService.GetAirParametersBetweenDates(DateTimeConverter.ConvertToUtc(since),
                DateTimeConverter.ConvertToUtc(until));

            if (records.Count == 0) return NotFound();

            return new HistoricalDataDto(records,
                (GroupingValues)Enum.Parse(typeof(GroupingValues), grouping),
                includeSummary,
                includeMeasurements);
        }
    }
}