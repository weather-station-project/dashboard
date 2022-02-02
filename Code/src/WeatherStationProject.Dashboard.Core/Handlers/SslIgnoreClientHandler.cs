using System.Net.Http;

namespace WeatherStationProject.Dashboard.Core.Handlers
{
    public class SslIgnoreClientHandler : HttpClientHandler
    {
        public SslIgnoreClientHandler()
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        }
    }
}