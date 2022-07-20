using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.GroundTemperatureService.Services;
using WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers
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
    }
}