using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Core.Configuration;

namespace WeatherStationProject.Dashboard.AirParametersService.Data
{
    public class AirParametersDbContext : DbContext
    {
        public virtual DbSet<AirParameters> AirParameters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfiguration.DatabaseConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}