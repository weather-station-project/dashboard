using System;

namespace WeatherStationProject.Dashboard.Core.Configuration
{
    public static class Audience
    {
        private const string AudienceSecretVariableName = "AUDIENCE_SECRET";

        public static string Secret => Environment.GetEnvironmentVariable(AudienceSecretVariableName);

        public static string Issuer => "Weather Station Project";

        public static string ValidAudience => "localhost";
    }
}