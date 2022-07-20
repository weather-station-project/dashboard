using System.Collections.Generic;
using WeatherStationProject.Dashboard.Data;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.Data.ViewModel;

namespace WeatherStationProject.Dashboard.Tests.Data
{
    public class GroupedDtoMock : GroupedDto<Measurement>
    {
        protected override Dictionary<string, List<Measurement>>? GroupEntities(List<Measurement> entities,
            GroupingValues grouping, bool includeSummary, bool includeMeasurements)
        {
            return null;
        }

        protected override void PopulateGroupedSummaries(Dictionary<string, List<Measurement>> groupedEntities)
        {
        }

        public string GetGroupingKeyPublic(Measurement entity, GroupingValues grouping)
        {
            return GetGroupingKey(entity, grouping);
        }
    }
}