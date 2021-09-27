using System;

namespace WeatherStationProject.Dashboard.Core.Configuration
{
    public static class AppConfiguration
    {
        // Database connection variables
        private const string DatabaseServerVariableName = "SERVER";
        private const string DatabaseNameVariableName = "DATABASE";
        private const string DatabaseUserVariableName = "USER";
        private const string DatabasePasswordVariableName = "PASSWORD";
        
        // React variables
        private const string AccuWeatherApiKeyVariableName = "ACCUWEATHER_API_KEY";
        private const string AccuWeatherLocationNameVariableName = "ACCUWEATHER_LOCATION_NAME";
        private const string WeatherApiHostVariableName = "WEATHER_API_HOST";
        private const string AuthenticationServiceHostVariableName = "AUTHENTICATION_SERVICE_HOST";
        
        // Variable to set the authentication secret of the application
        private const string AuthenticationSecretVariableName = "AUTHENTICATION_SECRET";

        public static string DatabaseConnectionString =>
            $"Host={Environment.GetEnvironmentVariable(DatabaseServerVariableName)};" +
            $"Database={Environment.GetEnvironmentVariable(DatabaseNameVariableName)};" +
            $"Username={Environment.GetEnvironmentVariable(DatabaseUserVariableName)};" +
            $"Password={Environment.GetEnvironmentVariable(DatabasePasswordVariableName)}";

        public static string AuthenticationSecret =>
            Environment.GetEnvironmentVariable(AuthenticationSecretVariableName);
        
        public static string AccuWeatherApiKey =>
            Environment.GetEnvironmentVariable(AccuWeatherApiKeyVariableName);
        
        public static string AccuWeatherLocationName =>
            Environment.GetEnvironmentVariable(AccuWeatherLocationNameVariableName);
        
        public static string WeatherApiHost =>
            Environment.GetEnvironmentVariable(WeatherApiHostVariableName);
        
        public static string AuthenticationServiceHost =>
            Environment.GetEnvironmentVariable(AuthenticationServiceHostVariableName);
    }
}