using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherStationProject.Dashboard.App.Attributes;
using WeatherStationProject.Dashboard.App.Handlers;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Core.Model;

namespace WeatherStationProject.Dashboard.App.Controllers
{
    [ApiController]
    [Route("api/weather-measurements")]
    [AllowOnlyFromLocalhostLocalhost]
    public class ApiProxyController : ControllerBase
    {
        private const string LastMeasurementsEndPoint = "/api/v1/weather-measurements/last";
        private const string AuthenticationServiceEndPoint = "/api/v1/authentication/";

        [HttpGet("last")]
        public async Task<IActionResult> LastMeasurements()
        {
            var authToken = await GetAuthToken();

            return null == authToken
                ? StatusCode(500, "Auth token could not be retrieved")
                : Ok(await GetLastMeasurements(authToken));
        }

        private async Task<string> GetAuthToken()
        {
            using var client = new HttpClient(new SslIgnoreClientHandler());
            var response = await client.GetAsync(AppConfiguration.AuthenticationServiceHost +
                                                 AuthenticationServiceEndPoint +
                                                 AppConfiguration.AuthenticationSecret);

            if (!response.IsSuccessStatusCode) return null;

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthenticationToken>(jsonString).AccessToken;
        }

        private async Task<string> GetLastMeasurements(string authToken)
        {
            using var client = new HttpClient(new SslIgnoreClientHandler());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var response = await client.GetAsync(AppConfiguration.WeatherApiHost + LastMeasurementsEndPoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}