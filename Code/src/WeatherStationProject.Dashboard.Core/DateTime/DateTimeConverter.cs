using System;

namespace WeatherStationProject.Dashboard.Core.DateTime
{
    public static class DateTimeConverter
    {
        public static System.DateTime ConvertToUtc(System.DateTime dateTime)
        {
            if (dateTime.Kind.Equals(DateTimeKind.Utc))
            {
                return dateTime;
            }

            return new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
                dateTime.Second, DateTimeKind.Utc);
        }
    }
}