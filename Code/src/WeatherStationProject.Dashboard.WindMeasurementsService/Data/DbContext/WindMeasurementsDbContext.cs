using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Core.Configuration;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Data
{
    public class WindMeasurementsDbContext : DbContext
    {
        public virtual DbSet<WindMeasurements> WindMeasurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfiguration.DatabaseConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}