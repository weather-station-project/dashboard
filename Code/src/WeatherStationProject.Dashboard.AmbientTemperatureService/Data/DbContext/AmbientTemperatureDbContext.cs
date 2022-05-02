using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Core.Configuration;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Data
{
    public class AmbientTemperatureDbContext : DbContext
    {
        public virtual DbSet<AmbientTemperature> AmbientTemperatures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfiguration.DatabaseConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}