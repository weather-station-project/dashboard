using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.App.Handlers;
using WeatherStationProject.Dashboard.Core.Configuration;

namespace WeatherStationProject.Dashboard.App.Controllers
{
    [ApiController]
    [Route("api/accu-weather")]
    public class AccuweatherProxyController : ControllerBase
    {
        private const string LocationKeyByCityNameEndPoint = "https://dataservice.accuweather.com/locations/v1/search";
        private const string CurrentConditionsEndPoint = "https://dataservice.accuweather.com/currentconditions/v1/";
        private const string ForecastDataEndPoint = "https://dataservice.accuweather.com/forecasts/v1/daily/5day/";

        [HttpGet("location-key/{language}")]
        public async Task<string> GetLocationKey(string language)
        {
            return await GetResponseDataByQuery(LocationKeyByCityNameEndPoint,
                $"apikey={AppConfiguration.AccuWeatherApiKey}&q={AppConfiguration.AccuWeatherLocationName}&details=false&language={language}");
        }

        private static async Task<string> GetResponseDataByQuery(string endpoint, string query)
        {
            var builder = new UriBuilder(endpoint) {Query = query};

            using var client = new HttpClient(new SslIgnoreClientHandler());
            var response = await client.GetAsync(builder.Uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        [HttpGet("current-conditions/{locationKey}/{language}")]
        public async Task<string> GetCurrentConditions(string locationKey, string language)
        {
            return await GetResponseDataByQuery(CurrentConditionsEndPoint + locationKey,
                $"apikey={AppConfiguration.AccuWeatherApiKey}&details=true&language={language}");
        }

        [HttpGet("forecast-data/{locationKey}/{language}")]
        public async Task<string> GetForecast(string locationKey, string language)
        {
            return await GetResponseDataByQuery(ForecastDataEndPoint + locationKey,
                $"apikey={AppConfiguration.AccuWeatherApiKey}&details=true&language={language}&metric=true");
        }
    }
}