using AutoMapper;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AmbientTemperature, AmbientTemperatureDto>();
        }
    }
}
