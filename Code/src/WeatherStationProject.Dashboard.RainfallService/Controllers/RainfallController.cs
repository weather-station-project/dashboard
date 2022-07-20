using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}