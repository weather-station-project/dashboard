using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.Data.ViewModel;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel
{
    public sealed class HistoricalDataDto : GroupedDto<GroundTemperature>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<GroundTemperaturesSummaryDto> SummaryByGroupingItem { get; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<GroundTemperatureDto> Measurements { get; }

        public HistoricalDataDto (
            List<GroundTemperature> entities,
            GroupingValues grouping,
            bool includeSummary,
            bool includeMeasurements)
        {
            if (includeSummary)
            {
                SummaryByGroupingItem = new List<GroundTemperaturesSummaryDto>();
            }

            if (includeMeasurements)
            {
                Measurements = new List<GroundTemperatureDto>();
            }
            
            var groupedEntities = GroupEntities(entities,
                grouping,
                includeSummary,
                includeMeasurements);

            PopulateGroupedSummaries(groupedEntities);
        }
        
        protected override Dictionary<string, List<GroundTemperature>> GroupEntities(List<GroundTemperature> entities,
            GroupingValues grouping,
            bool includeSummary,
            bool includeMeasurements)
        {
            var groupedEntities = new Dictionary<string, List<GroundTemperature>>();

            foreach (var entity in entities)
            {
                if (includeMeasurements) Measurements.Add(GroundTemperatureDto.FromEntity(entity));

                if (!includeSummary) continue;

                var key = GetGroupingKey(entity, grouping);
                if (groupedEntities.ContainsKey(key))
                    groupedEntities[key].Add(entity);
                else
                    groupedEntities.Add(key, new List<GroundTemperature> {entity});
            }

            return groupedEntities;
        }

        protected override void PopulateGroupedSummaries(Dictionary<string, List<GroundTemperature>> groupedEntities)
        {
            foreach (var (key, value) in groupedEntities)
            {
                SummaryByGroupingItem.Add(new GroundTemperaturesSummaryDto
                {
                    Key = key,
                    MaxTemperature = value.Max(x => x.Temperature),
                    AvgTemperature = value.Average(x => x.Temperature),
                    MinTemperature = value.Min(x => x.Temperature)
                });
            }
        }
    }
}