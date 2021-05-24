using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.RainfallService.Data
{
    public class RainfallDbContext : WeatherStationDatabaseContext
    {
        private readonly IAppConfiguration _appConfiguration;

        public RainfallDbContext(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public virtual DbSet<Rainfall> Rainfall { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString: _appConfiguration.DatabaseConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
