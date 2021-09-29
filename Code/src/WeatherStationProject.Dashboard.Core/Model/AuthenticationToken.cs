namespace WeatherStationProject.Dashboard.Core.Model
{
    public class AuthenticationToken
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}