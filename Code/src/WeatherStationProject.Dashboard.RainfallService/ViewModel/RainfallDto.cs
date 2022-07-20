using System;

namespace WeatherStationProject.Dashboard.RainfallService.ViewModel
{
    public class RainfallDto
    {
        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }

        public decimal Amount { get; set; }

        public static RainfallDto FromEntity(decimal amount, DateTime since, DateTime until)
        {
            return new RainfallDto
            {
                FromDateTime = since.ToLocalTime(),
                ToDateTime = until.ToLocalTime(),
                Amount = amount
            };
        }
    }
}