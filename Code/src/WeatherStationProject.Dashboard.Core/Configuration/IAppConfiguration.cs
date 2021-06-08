namespace WeatherStationProject.Dashboard.Core.Configuration
{
    public interface IAppConfiguration
    {
        string DatabaseConnectionString { get; }

        string HashedAuthenticationSecret { get; }

        IAudience Audience { get; }
    }
}
