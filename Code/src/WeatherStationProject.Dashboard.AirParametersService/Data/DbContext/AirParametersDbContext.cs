using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.Data
{
    public class AirParametersDbContext : WeatherStationDatabaseContext
    {
        private readonly IAppConfiguration _appConfiguration;

        public AirParametersDbContext(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public virtual DbSet<AirParameters> AirParameters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString: _appConfiguration.DatabaseConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
