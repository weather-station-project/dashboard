using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Services;
using WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel;

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
        public async Task<ActionResult<AmbientTemperatureDTO>> LastMeasurement()
        {
            var last = await _ambientTemperatureService.GetLastTemperature();

            if (null == last) return NotFound();

            return AmbientTemperatureDTO.FromEntity(last);
        }
    }
}