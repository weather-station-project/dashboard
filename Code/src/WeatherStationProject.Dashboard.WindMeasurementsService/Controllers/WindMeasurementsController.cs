using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeatherStationProject.Dashboard.WindMeasurementsService.Services;
using WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route(template: "api/v{version:apiVersion}/wind-measurements")]
    public class WindMeasurementsController : ControllerBase
    {
        private readonly IWindMeasurementsService _windMeasurementsService;

        public WindMeasurementsController(IWindMeasurementsService windMeasurementsService)
        {
            _windMeasurementsService = windMeasurementsService;
        }

        [HttpGet(template: "last")]
        public async Task<ActionResult<WindMeasurementsDTO>> LastMeasurement()
        {
            var last = await _windMeasurementsService.GetLastWindMeasurements();

            if (null == last)
            {
                return NotFound();
            }

            return WindMeasurementsDTO.FromEntity(last);
        }

        [HttpGet(template: "gust-in-time/{minutes}")]
        public async Task<ActionResult<WindMeasurementsDTO>> GustInTime([Required, Range(15, 60)] int minutes)
        {
            var gust = await _windMeasurementsService.GetGustInTime(minutes: minutes);

            if (null == gust)
            {
                return NotFound();
            }

            return WindMeasurementsDTO.FromEntity(gust);
        }
    }
}
