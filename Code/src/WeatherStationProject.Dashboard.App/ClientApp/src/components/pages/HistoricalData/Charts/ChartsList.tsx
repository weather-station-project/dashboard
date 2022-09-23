import React from 'react';
import axios, { AxiosInstance } from 'axios';
import BarAndLineChart from './BarAndLineChart';
import { useTranslation } from 'react-i18next';
import WindMeasurementsChart from './WindMeasurementsChart';
import {
  ChartValues,
  IHistoricalDataRequest,
  IHistoricalDataResult,
  IPredominantDirection,
} from '../../../../model/HistoricalDataTypes';
import {
  blueColor,
  blueColorAlpha,
  darkBlueColor,
  darkBlueColorAlpha,
  redColor,
  redColorAlpha,
  yellowColor,
  yellowColorAlpha,
} from '../ChartsAndListsConstants';
import Loading from '../../../Loading';

interface IChartsListProps {
  requestData: IHistoricalDataRequest;
  reRenderForcedState: number;
}

const ChartsList: React.FC<IChartsListProps> = ({ requestData, reRenderForcedState }) => {
  const { t } = useTranslation();
  const shouldFetch = React.useRef(true);
  const [data, setData] = React.useState({} as IHistoricalDataResult);
  const [loading, setLoading] = React.useState<boolean>(true);
  const url = '/api/weather-measurements/historical';
  const sectionSeparation = { marginTop: 75 };
  const chartType = requestData.chartView === ChartValues.Lines ? 'line' : 'bar';

  React.useEffect(() => {
    async function fetchData() {
      if (shouldFetch.current) {
        shouldFetch.current = false;
      } else {
        return;
      }

      setLoading(true);

      const api: AxiosInstance = axios.create({
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        params: {
          since: requestData.initialDate,
          until: requestData.finalDate,
          grouping: requestData.grouping,
          includeSummary: true,
          includeMeasurements: false,
        },
        method: 'GET',
        baseURL: '/',
      });

      api
        .get<string>(url)
        .then((response) => {
          setData(JSON.parse(response.data) as IHistoricalDataResult);
        })
        .catch((e) => {
          setData((() => {
            throw e;
          }) as never);
        })
        .finally(() => {
          shouldFetch.current = true;
          setLoading(false);
        });
    }

    fetchData();
  }, [reRenderForcedState]);

  return (
    <>
      {loading ? (
        <Loading />
      ) : (
        <>
          <h2>{t('historical_data.chart.air_parameters')}</h2>
          <BarAndLineChart
            chartType={chartType}
            chartTitle={t('historical_data.chart.air_parameters.air_pressure')}
            maxTitle={t('historical_data.chart.max')}
            avgTitle={t('historical_data.chart.avg')}
            minTitle={t('historical_data.chart.min')}
            maxBgColor={darkBlueColorAlpha}
            maxBorderColor={darkBlueColor}
            avgBgColor={yellowColorAlpha}
            avgBorderColor={yellowColor}
            minBgColor={blueColorAlpha}
            minBorderColor={blueColor}
            keys={data.airParameters.summaryByGroupingItem?.map((item) => item.key) as string[]}
            maxValues={data.airParameters.summaryByGroupingItem?.map((item) => item.maxPressure) as number[]}
            avgValues={data.airParameters.summaryByGroupingItem?.map((item) => item.avgPressure) as number[]}
            minValues={data.airParameters.summaryByGroupingItem?.map((item) => item.minPressure) as number[]}
          />
          <div className="mt-5">
            <BarAndLineChart
              chartType={chartType}
              chartTitle={t('historical_data.chart.air_parameters.humidity')}
              maxTitle={t('historical_data.chart.max')}
              avgTitle={t('historical_data.chart.avg')}
              minTitle={t('historical_data.chart.min')}
              maxBgColor={darkBlueColorAlpha}
              maxBorderColor={darkBlueColor}
              avgBgColor={yellowColorAlpha}
              avgBorderColor={yellowColor}
              minBgColor={blueColorAlpha}
              minBorderColor={blueColor}
              keys={data.airParameters.summaryByGroupingItem?.map((item) => item.key) as string[]}
              maxValues={data.airParameters.summaryByGroupingItem?.map((item) => item.maxHumidity) as number[]}
              avgValues={data.airParameters.summaryByGroupingItem?.map((item) => item.avgHumidity) as number[]}
              minValues={data.airParameters.summaryByGroupingItem?.map((item) => item.minHumidity) as number[]}
            />
          </div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.temperatures')}</h2>
            <BarAndLineChart
              chartType={chartType}
              chartTitle={t('historical_data.chart.temperatures.ambient')}
              maxTitle={t('historical_data.chart.max')}
              avgTitle={t('historical_data.chart.avg')}
              minTitle={t('historical_data.chart.min')}
              maxBgColor={redColorAlpha}
              maxBorderColor={redColor}
              avgBgColor={yellowColorAlpha}
              avgBorderColor={yellowColor}
              minBgColor={blueColorAlpha}
              minBorderColor={blueColor}
              keys={data.ambientTemperatures.summaryByGroupingItem?.map((item) => item.key) as string[]}
              maxValues={data.ambientTemperatures.summaryByGroupingItem?.map((item) => item.maxTemperature) as number[]}
              avgValues={data.ambientTemperatures.summaryByGroupingItem?.map((item) => item.avgTemperature) as number[]}
              minValues={data.ambientTemperatures.summaryByGroupingItem?.map((item) => item.minTemperature) as number[]}
            />
          </div>
          <div className="mt-5">
            <BarAndLineChart
              chartType={chartType}
              chartTitle={t('historical_data.chart.temperatures.ground')}
              maxTitle={t('historical_data.chart.max')}
              avgTitle={t('historical_data.chart.avg')}
              minTitle={t('historical_data.chart.min')}
              maxBgColor={redColorAlpha}
              maxBorderColor={redColor}
              avgBgColor={yellowColorAlpha}
              avgBorderColor={yellowColor}
              minBgColor={blueColorAlpha}
              minBorderColor={blueColor}
              keys={data.groundTemperatures.summaryByGroupingItem?.map((item) => item.key) as string[]}
              maxValues={data.groundTemperatures.summaryByGroupingItem?.map((item) => item.maxTemperature) as number[]}
              avgValues={data.groundTemperatures.summaryByGroupingItem?.map((item) => item.avgTemperature) as number[]}
              minValues={data.groundTemperatures.summaryByGroupingItem?.map((item) => item.minTemperature) as number[]}
            />
          </div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.rainfall')}</h2>
            <BarAndLineChart
              chartType={chartType}
              chartTitle={t('historical_data.chart.rainfall.chart')}
              maxTitle={t('historical_data.chart.max')}
              avgTitle={t('historical_data.chart.avg')}
              minTitle={t('historical_data.chart.min')}
              maxBgColor={darkBlueColorAlpha}
              maxBorderColor={darkBlueColor}
              avgBgColor={blueColorAlpha}
              avgBorderColor={blueColor}
              minBgColor={yellowColorAlpha}
              minBorderColor={yellowColor}
              keys={data.rainfall.summaryByGroupingItem?.map((item) => item.key) as string[]}
              maxValues={data.rainfall.summaryByGroupingItem?.map((item) => item.maxAmount) as number[]}
              avgValues={data.rainfall.summaryByGroupingItem?.map((item) => item.avgAmount) as number[]}
              minValues={data.rainfall.summaryByGroupingItem?.map((item) => item.minAmount) as number[]}
            />
          </div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.wind_measurements')}</h2>
            <WindMeasurementsChart
              chartType={chartType}
              speedChartTitle={t('historical_data.chart.wind_measurements.speed')}
              directionChartTitle={t('historical_data.chart.wind_measurements.direction')}
              avgSpeedTitle={t('historical_data.chart.wind_measurements.speed.avg')}
              gustTitle={t('historical_data.chart.wind_measurements.speed.gust')}
              keys={data.windMeasurements.summaryByGroupingItem?.map((item) => item.key) as string[]}
              avgSpeedValues={data.windMeasurements.summaryByGroupingItem?.map((item) => item.avgSpeed) as number[]}
              gustValues={data.windMeasurements.summaryByGroupingItem?.map((item) => item.maxGust) as number[]}
              directionValues={data.windMeasurements.predominantWindDirections as IPredominantDirection}
            />
          </div>
        </>
      )}
    </>
  );
};

export default ChartsList;
