using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherStationProject.Dashboard.App.Handlers;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Core.Model;

namespace WeatherStationProject.Dashboard.App.Controllers
{
    [ApiController]
    [Route("api/weather-measurements")]
    public class ApiProxyController : ControllerBase
    {
        private const string LastMeasurementsEndPoint = "/api/v1/weather-measurements/last";
        private const string AuthenticationServiceEndPoint = "/api/v1/authentication/";

        private readonly HttpMessageHandler _httpHandler = new SslIgnoreClientHandler();

        public ApiProxyController()
        {
        }

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

        private async Task<string> GetAuthToken()
        {
            using var client = new HttpClient(_httpHandler);
            var response = await client.GetAsync(AppConfiguration.AuthenticationServiceHost +
                                                 AuthenticationServiceEndPoint +
                                                 AppConfiguration.AuthenticationSecret);

            if (!response.IsSuccessStatusCode) return null;

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthenticationToken>(jsonString).AccessToken;
        }

        private async Task<string> GetLastMeasurements(string authToken)
        {
            using var client = new HttpClient(_httpHandler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var response = await client.GetAsync(AppConfiguration.WeatherApiHost + LastMeasurementsEndPoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}