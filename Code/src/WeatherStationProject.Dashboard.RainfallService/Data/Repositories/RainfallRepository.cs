using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.RainfallService.Data
{
    public class RainfallRepository : Repository<Rainfall>, IRainfallRepository
    {
        private readonly RainfallDbContext _rainfallDbContext;

        public RainfallRepository(RainfallDbContext rainfallDbContext) : base(rainfallDbContext)
        {
            _rainfallDbContext = rainfallDbContext;
        }

        public async Task<decimal> GetRainfallDuringTime(DateTime since, DateTime until)
        {
            return await _rainfallDbContext.Rainfall
                .Where(x => x.DateTime >= since && x.DateTime <= until)
                .SumAsync(x => x.Amount);
        }
    }
}