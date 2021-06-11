using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeatherStationProject.Dashboard.GroundTemperatureService.Services;
using WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route(template: "api/v{version:apiVersion}/ground-temperatures")]
    public class GroundTemperatureController : ControllerBase
    {
        private readonly IGroundTemperatureService _groundTemperatureService;

        public GroundTemperatureController(IGroundTemperatureService groundTemperatureService)
        {
            _groundTemperatureService = groundTemperatureService;
        }

        [HttpGet(template: "last")]
        public async Task<ActionResult<GroundTemperatureDTO>> LastMeasurement()
        {
            var last = await _groundTemperatureService.GetLastTemperature();

            if (null == last)
            {
                return NotFound();
            }

            return GroundTemperatureDTO.FromEntity(last);
        }
    }
}
