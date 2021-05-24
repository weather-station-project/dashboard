using System;

namespace WeatherStationProject.Dashboard.RainfallService.ViewModel
{
    public class RainfallDTO
    {
        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }

        public decimal Amount { get; set; }

        public static RainfallDTO FromEntity(decimal amount, DateTime since, DateTime until)
        {
            return new()
            {
                FromDateTime = since,
                ToDateTime = until,
                Amount = amount,
            };
        }
    }
}
