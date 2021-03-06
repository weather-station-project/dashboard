using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.AirParametersService.Services;
using WeatherStationProject.Dashboard.AirParametersService.ViewModel;

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
    }
}