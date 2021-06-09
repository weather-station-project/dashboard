namespace WeatherStationProject.Dashboard.Core.Configuration
{
    public interface IAppConfiguration
    {
        string DatabaseConnectionString { get; }

        string AuthenticationSecret { get; }

        IAudience Audience { get; }
    }
}
