using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.Core.DateTime;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.RainfallService.Services;
using WeatherStationProject.Dashboard.RainfallService.ViewModel;

namespace WeatherStationProject.Dashboard.RainfallService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/rainfall")]
    public class RainfallController : ControllerBase
    {
        private readonly IRainfallService _rainfallService;

        public RainfallController(IRainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        [HttpGet("amount-during-time/{minutes}")]
        public async Task<ActionResult<RainfallDto>> RainfallDuringTime([Required] [Range(15, 60)] int minutes)
        {
            var until = DateTime.UtcNow;
            var since = until.AddMinutes(-minutes);

            var amount = await _rainfallService.GetRainfallDuringTime(since, until);
            return RainfallDto.FromEntity(amount, since, until);
        }
        
        [HttpGet("historical")]
        public async Task<ActionResult<HistoricalDataDto>> HistoricalData(
            [Required] DateTime since,
            [Required] DateTime until,
            [Required] [GroupingRange] string grouping,
            [Required] bool includeSummary,
            [Required] bool includeMeasurements)
        {
            var records = await _rainfallService.GetRainfallMeasurementsBetweenDates(DateTimeConverter.ConvertToUtc(since),
                DateTimeConverter.ConvertToUtc(until));

            if (records.Count == 0) return NotFound();

            return new HistoricalDataDto(records,
                (GroupingValues)Enum.Parse(typeof(GroupingValues), grouping),
                includeSummary,
                includeMeasurements);
        }
    }
}