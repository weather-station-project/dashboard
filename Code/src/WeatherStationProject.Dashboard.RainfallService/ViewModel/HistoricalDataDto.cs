using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.Data.ViewModel;
using WeatherStationProject.Dashboard.RainfallService.Data;

namespace WeatherStationProject.Dashboard.RainfallService.ViewModel
{
    public class HistoricalDataDto : GroupedDto<Rainfall>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, SummaryDto> SummaryByGroupingItem { get; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RainfallDto> Measurements { get; }

        public HistoricalDataDto (
            List<Rainfall> entities,
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
                Measurements = new List<RainfallDto>();
            }
            
            var groupedEntities = GroupEntities(entities,
                grouping,
                includeSummary,
                includeMeasurements);

            PopulateGroupedSummaries(groupedEntities);
        }
        
        protected override Dictionary<string, List<Rainfall>> GroupEntities(List<Rainfall> entities,
            GroupingValues grouping,
            bool includeSummary,
            bool includeMeasurements)
        {
            var groupedEntities = new Dictionary<string, List<Rainfall>>();

            foreach (var entity in entities)
            {
                if (includeMeasurements) Measurements.Add(RainfallDto.FromEntity(entity.Amount, entity.DateTime, entity.DateTime));

                if (!includeSummary) continue;

                var key = GetGroupingKey(entity, grouping);
                if (groupedEntities.ContainsKey(key))
                    groupedEntities[key].Add(entity);
                else
                    groupedEntities.Add(key, new List<Rainfall> {entity});
            }

            return groupedEntities;
        }

        protected override void PopulateGroupedSummaries(Dictionary<string, List<Rainfall>> groupedEntities)
        {
            foreach (var (key, value) in groupedEntities)
            {
                var avg = value.Average(x => x.Amount);
                
                SummaryByGroupingItem.Add(key, new SummaryDto
                {
                    AmountAvg = avg
                });
            }
        }
    }
}