namespace WeatherStationProject.Dashboard.Core.Configuration
{
    public interface IAudience
    {
        string Secret { get; }

        string Issuer { get; }

        string ValidAudience { get; }
    }
}
