using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeatherStationProject.Dashboard.RainfallService.Services;
using WeatherStationProject.Dashboard.RainfallService.ViewModel;

namespace WeatherStationProject.Dashboard.RainfallService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route(template: "api/v{version:apiVersion}/rainfall")]
    public class WindMeasurementsController : ControllerBase
    {
        private readonly IRainfallService _rainfallService;

        public WindMeasurementsController(IRainfallService rainfallService)
        {
            _rainfallService = rainfallService;
        }

        [HttpGet(template: "amount-during-time/{minutes}")]
        public async Task<ActionResult<RainfallDTO>> RainfallDuringTime([Required, Range(15, 60)] int minutes)
        {
            var until = DateTime.Now;
            var since = until.AddMinutes(-minutes);

            var amount = await _rainfallService.GetRainfallDuringTime(since: since, until: until);
            return RainfallDTO.FromEntity(amount: amount, since: since, until: until);
        }
    }
}
