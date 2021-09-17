using System;

namespace WeatherStationProject.Dashboard.Core.Configuration
{
    public static class AppConfiguration
    {
        private const string DatabaseServerVariableName = "SERVER";
        private const string DatabaseNameVariableName = "DATABASE";
        private const string DatabaseUserVariableName = "USER";
        private const string DatabasePasswordVariableName = "PASSWORD";
        private const string AuthenticationSecretVariableName = "AUTHENTICATION_SECRET";

        public static string DatabaseConnectionString =>
            $"Host={Environment.GetEnvironmentVariable(DatabaseServerVariableName)};" +
            $"Database={Environment.GetEnvironmentVariable(DatabaseNameVariableName)};" +
            $"Username={Environment.GetEnvironmentVariable(DatabaseUserVariableName)};" +
            $"Password={Environment.GetEnvironmentVariable(DatabasePasswordVariableName)}";

        public static string AuthenticationSecret =>
            Environment.GetEnvironmentVariable(AuthenticationSecretVariableName);
    }
}