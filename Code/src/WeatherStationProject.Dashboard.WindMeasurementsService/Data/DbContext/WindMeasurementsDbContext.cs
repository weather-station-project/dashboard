using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Data
{
    public class WindMeasurementsDbContext : WeatherStationDatabaseContext
    {
        private readonly IAppConfiguration _appConfiguration;

        public WindMeasurementsDbContext(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public virtual DbSet<WindMeasurements> WindMeasurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString: _appConfiguration.DatabaseConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
