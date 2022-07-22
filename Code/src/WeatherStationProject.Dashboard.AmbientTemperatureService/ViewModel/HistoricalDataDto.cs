using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel
{
    public sealed class HistoricalDataDto : GroupedDto<AmbientTemperature>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, SummaryDto> SummaryByGroupingItem { get; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AmbientTemperatureDto> Measurements { get; }

        public HistoricalDataDto (
            List<AmbientTemperature> entities,
            GroupingValues grouping,
            bool includeSummary,
            bool includeMeasurements)
        {
            if (includeSummary)
            {
                SummaryByGroupingItem = new Dictionary<string, SummaryDto>();
            }

            if (includeMeasurements)
            {
                Measurements = new List<AmbientTemperatureDto>();
            }
            
            var groupedEntities = GroupEntities(entities,
                grouping,
                includeSummary,
                includeMeasurements);

            PopulateGroupedSummaries(groupedEntities);
        }
        
        protected override Dictionary<string, List<AmbientTemperature>> GroupEntities(List<AmbientTemperature> entities,
            GroupingValues grouping,
            bool includeSummary,
            bool includeMeasurements)
        {
            var groupedEntities = new Dictionary<string, List<AmbientTemperature>>();

            foreach (var entity in entities)
            {
                if (includeMeasurements) Measurements.Add(AmbientTemperatureDto.FromEntity(entity));

                if (!includeSummary) continue;

                var key = GetGroupingKey(entity, grouping);
                if (groupedEntities.ContainsKey(key))
                    groupedEntities[key].Add(entity);
                else
                    groupedEntities.Add(key, new List<AmbientTemperature> {entity});
            }

            return groupedEntities;
        }

        protected override void PopulateGroupedSummaries(Dictionary<string, List<AmbientTemperature>> groupedEntities)
        {
            foreach (var (key, value) in groupedEntities)
            {
                SummaryByGroupingItem.Add(key, new SummaryDto
                {
                    MaxTemperature = value.Max(x => x.Temperature),
                    AvgTemperature = value.Average(x => x.Temperature),
                    MinTemperature = value.Min(x => x.Temperature)
                });
            }
        }
    }
}