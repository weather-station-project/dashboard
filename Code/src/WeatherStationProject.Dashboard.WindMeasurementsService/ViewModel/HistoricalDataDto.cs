using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.Data.ViewModel;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel
{
    public sealed class HistoricalDataDto : GroupedDto<WindMeasurements>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<WindMeasurementsSummaryDto> SummaryByGroupingItem { get; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<WindMeasurementsDto> Measurements { get; }

        public HistoricalDataDto (
            List<WindMeasurements> entities,
            GroupingValues grouping,
            bool includeSummary,
            bool includeMeasurements)
        {
            if (includeSummary)
            {
                SummaryByGroupingItem = new List<WindMeasurementsSummaryDto>();
            }

            if (includeMeasurements)
            {
                Measurements = new List<WindMeasurementsDto>();
            }
            
            var groupedEntities = GroupEntities(entities,
                grouping,
                includeSummary,
                includeMeasurements);

            PopulateGroupedSummaries(groupedEntities);
        }
        
        protected override Dictionary<string, List<WindMeasurements>> GroupEntities(List<WindMeasurements> entities,
            GroupingValues grouping,
            bool includeSummary,
            bool includeMeasurements)
        {
            var groupedEntities = new Dictionary<string, List<WindMeasurements>>();

            foreach (var entity in entities)
            {
                if (includeMeasurements) Measurements.Add(WindMeasurementsDto.FromEntity(entity));

                if (!includeSummary) continue;

                var key = GetGroupingKey(entity, grouping);
                if (groupedEntities.ContainsKey(key))
                    groupedEntities[key].Add(entity);
                else
                    groupedEntities.Add(key, new List<WindMeasurements> {entity});
            }

            return groupedEntities;
        }

        protected override void PopulateGroupedSummaries(Dictionary<string, List<WindMeasurements>> groupedEntities)
        {
            foreach (var (key, value) in groupedEntities)
            {
                var predominantDirection = value.GroupBy(x => x.Direction)
                    .OrderByDescending(c => c.Count())
                    .Take(1)
                    .First()
                    .Select(p => p.Direction).ToArray()[0];
                
                SummaryByGroupingItem.Add(new WindMeasurementsSummaryDto
                {
                    Key = key,
                    AvgSpeed = value.Average(x => x.Speed),
                    MaxGust = value.Max(x => x.Speed),
                    PredominantDirection = predominantDirection
                });
            }
        }
    }
}