using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Services;
using WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(template: "api/v{version:apiVersion}/[controller]")]
    public class AmbientTemperaturesController : ControllerBase
    {
        private readonly IAmbientTemperatureService _ambientTemperatureService;

        public AmbientTemperaturesController(IAmbientTemperatureService ambientTemperatureService)
        {
            _ambientTemperatureService = ambientTemperatureService;
        }

        [HttpGet(template:"last")]
        public async Task<ActionResult<AmbientTemperatureDTO>> LastMeasurement()
        {
            var last = await _ambientTemperatureService.GetLastTemperature();

            if (null == last)
            {
                return NotFound();
            }

            return AmbientTemperatureDTO.FromEntity(last);
        }
    }
}
