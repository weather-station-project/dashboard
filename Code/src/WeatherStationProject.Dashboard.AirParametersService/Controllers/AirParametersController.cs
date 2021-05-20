using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AirParametersService.Services;
using WeatherStationProject.Dashboard.AirParametersService.ViewModel;

namespace WeatherStationProject.Dashboard.AirParametersService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(template: "api/v{version:apiVersion}/[controller]")]
    public class AirParametersController : ControllerBase
    {
        private readonly IAirParametersService _airParametersService;

        public AirParametersController(IAirParametersService airParametersService)
        {
            _airParametersService = airParametersService;
        }

        [HttpGet(template: "last")]
        public async Task<ActionResult<AirParametersDTO>> LastMeasurement()
        {
            var last = await _airParametersService.GetLastTemperature();

            if (null == last)
            {
                return NotFound();
            }

            return AirParametersDTO.FromEntity(last);
        }
    }
}
