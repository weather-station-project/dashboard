using System;

namespace WeatherStationProject.Dashboard.Core.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        private const string DatabaseServerVariableName = "SERVER";
        private const string DatabaseNameVariableName = "DATABASE";
        private const string DatabaseUserVariableName = "USER";
        private const string DatabasePasswordVariableName = "PASSWORD";
        private const string HashedAuthenticationSecretVariableName = "AUTHENTICATION_SECRET_SHA256";

        public string DatabaseConnectionString => $"Host={Environment.GetEnvironmentVariable(DatabaseServerVariableName)};" +
                                                  $"Database={Environment.GetEnvironmentVariable(DatabaseNameVariableName)};" +
                                                  $"Username={Environment.GetEnvironmentVariable(DatabaseUserVariableName)};" +
                                                  $"Password={Environment.GetEnvironmentVariable(DatabasePasswordVariableName)}";

        public string HashedAuthenticationSecret => Environment.GetEnvironmentVariable(HashedAuthenticationSecretVariableName);

        public IAudience Audience => new Audience();
    }
}
