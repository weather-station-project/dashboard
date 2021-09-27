using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.App.Attributes;
using WeatherStationProject.Dashboard.App.Handlers;
using WeatherStationProject.Dashboard.Core.Configuration;

namespace WeatherStationProject.Dashboard.App.Controllers
{
    [ApiController]
    [Route("api/accu-weather")]
    [AllowOnlyFromLocalhostLocalhost]
    public class AccuweatherProxyController : ControllerBase
    {
        private const string LocationKeyByCityNameEndPoint = "https://dataservice.accuweather.com/locations/v1/search";
        private const string CurrentConditionsEndPoint = "https://dataservice.accuweather.com/currentconditions/v1/";
        private const string ForecastDataEndPoint = "https://dataservice.accuweather.com/forecasts/v1/daily/5day/";
        
        [HttpGet("location-key/{language}")]
        public async Task<IActionResult> GetLocationKey(string language)
        {
            var locationKey = await GetLocationKeyByCityName(language);
            
            return null == locationKey
                ? StatusCode(500, $"Location key for the city '{AppConfiguration.AccuWeatherLocationName}' could not be retrieved")
                : Ok(await GetLocationKeyByCityName(language));
        }

        private async Task<string> GetLocationKeyByCityName(string language)
        {
            var builder = new UriBuilder(LocationKeyByCityNameEndPoint)
            {
                Query = $"apikey={AppConfiguration.AccuWeatherApiKey}&q={AppConfiguration.AccuWeatherLocationName}&details=false&language={language}"
            };

            using var client = new HttpClient(new SslIgnoreClientHandler());
            var response = await client.GetAsync(builder.Uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        [HttpGet("current-conditions/{locationKey}/{language}")]
        public async Task<IActionResult> GetCurrentConditions(string locationKey, string language)
        {
            var currentConditions = await GetCurrentConditionsData(locationKey, language);
            
            return null == currentConditions
                ? StatusCode(500, $"Current conditions for the city '{AppConfiguration.AccuWeatherLocationName}' and location key '{locationKey}' could not be retrieved")
                : Ok(currentConditions);
        }

        private async Task<string> GetCurrentConditionsData(string locationKey, string language)
        {
            var builder = new UriBuilder(CurrentConditionsEndPoint + locationKey)
            {
                Query = $"apikey={AppConfiguration.AccuWeatherApiKey}&details=true&language={language}"
            };

            using var client = new HttpClient(new SslIgnoreClientHandler());
            var response = await client.GetAsync(builder.Uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        
        [HttpGet("forecast-data/{locationKey}/{language}")]
        public async Task<IActionResult> GetForecast(string locationKey, string language)
        {
            var data = await GetForecastData(locationKey, language);
            
            return null == data
                ? StatusCode(500, $"Forecast data for the city '{AppConfiguration.AccuWeatherLocationName}' and location key '{locationKey}' could not be retrieved")
                : Ok(data);
        }

        private async Task<string> GetForecastData(string locationKey, string language)
        {
            var builder = new UriBuilder(ForecastDataEndPoint + locationKey)
            {
                Query = $"apikey={AppConfiguration.AccuWeatherApiKey}&details=true&language={language}"
            };

            using var client = new HttpClient(new SslIgnoreClientHandler());
            var response = await client.GetAsync(builder.Uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}