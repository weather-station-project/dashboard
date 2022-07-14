using System.Collections.Generic;
using WeatherStationProject.Dashboard.Data.Validations;

namespace WeatherStationProject.Dashboard.Data.ViewModel
{
    public abstract class GroupedDTO<T> where T : Measurement
    {
        public List<MeasurementDTO> Measurements;
        
        protected abstract Dictionary<string, List<T>> GroupEntities(
            List<T> entities,
            GroupingValues grouping,
            bool includeSummary,
            bool includeMeasurements);

        protected abstract void PopulateGroupedSummaries(Dictionary<string, List<T>> groupedEntities);

        protected string GetGroupingKey(Measurement entity, GroupingValues grouping)
        {
            return grouping switch
            {
                GroupingValues.Hours => $"{entity.DateTime:yyyy-MM-dd/HH}",
                GroupingValues.Days => $"{entity.DateTime:yyyy-MM-dd}",
                GroupingValues.Months => $"{entity.DateTime.Year}-{entity.DateTime:MM}",
                _ => string.Empty
            };
        }
    }
}