using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Core.Model;
using WeatherStationProject.Dashboard.Data.Validations;

namespace WeatherStationProject.Dashboard.App.Controllers
{
    [ApiController]
    [Route("api/weather-measurements")]
    public class ApiProxyController : ControllerBase
    {
        private const string LastMeasurementsEndPoint = "/api/v1/weather-measurements/last";
        private const string HistoricalDataEndPoint = "/api/v1/historical-weather-measurements/historical";
        private const string AuthenticationServiceEndPoint = "/api/v1/authentication/";
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        private readonly HttpMessageHandler _httpHandler;

        public ApiProxyController(HttpMessageHandler handler)
        {
            _httpHandler = handler;
        }

        [HttpGet("last")]
        public async Task<ObjectResult> LastMeasurements()
        {
            var authToken = await GetAuthToken();

            return null == authToken
                ? StatusCode(500, "Auth token could not be retrieved")
                : Ok(await GetLastMeasurements(authToken));
        }

        [HttpGet("historical")]
        public async Task<ObjectResult> HistoricalData(
            [Required] DateTime since,
            [Required] DateTime until,
            [Required] [GroupingRange] string grouping,
            [Required] bool includeSummary,
            [Required] bool includeMeasurements)
        {
            var authToken = await GetAuthToken();

            return null == authToken
                ? StatusCode(500, "Auth token could not be retrieved")
                : Ok(await GetHistoricalData(authToken, since, until, grouping, includeSummary, includeMeasurements));
        }

        private async Task<string> GetAuthToken()
        {
            using var client = new HttpClient(_httpHandler, false);
            var response = await client.GetAsync(AppConfiguration.AuthenticationServiceHost +
                                                 AuthenticationServiceEndPoint +
                                                 AppConfiguration.AuthenticationSecret);

            if (!response.IsSuccessStatusCode) return null;

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthenticationToken>(jsonString)?.AccessToken;
        }

        private async Task<string> GetLastMeasurements(string authToken)
        {
            using var client = new HttpClient(_httpHandler, false);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var response = await client.GetAsync(AppConfiguration.WeatherApiHost + LastMeasurementsEndPoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetHistoricalData(
            string authToken,
            DateTime since,
            DateTime until,
            string grouping,
            bool includeSummary,
            bool includeMeasurements)
        {
            using var client = new HttpClient(_httpHandler, false);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var parameters = new Dictionary<string, string>
            {
                {"since", since.ToString(DateTimeFormat)},
                {"until", until.ToString(DateTimeFormat)},
                {"grouping", grouping},
                {"includeSummary", includeSummary.ToString()},
                {"includeMeasurements", includeMeasurements.ToString()}
            };
            var response = await client.GetAsync(new Uri(
                QueryHelpers.AddQueryString(AppConfiguration.WeatherApiHost + HistoricalDataEndPoint, parameters)));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}