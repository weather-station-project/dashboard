using System.Net.Http;

namespace WeatherStationProject.Dashboard.App.Handlers
{
    public class SslIgnoreClientHandler : HttpClientHandler
    {
        public SslIgnoreClientHandler()
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        }
    }
}