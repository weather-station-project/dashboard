using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.RainfallService.Data
{
    public class RainfallDbContext : DbContext
    {
        public virtual DbSet<Rainfall> Rainfall { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(AppConfiguration.DatabaseConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}