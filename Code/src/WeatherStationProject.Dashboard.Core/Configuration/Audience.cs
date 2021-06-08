using System;

namespace WeatherStationProject.Dashboard.Core.Configuration
{
    public class Audience : IAudience
    {
        private const string AudienceSecretVariableName = "AUDIENCE_SECRET";

        public string Secret => Environment.GetEnvironmentVariable(AudienceSecretVariableName);

        public string Issuer => "Weather Station Project";

        public string ValidAudience => "localhost";
    }
}
