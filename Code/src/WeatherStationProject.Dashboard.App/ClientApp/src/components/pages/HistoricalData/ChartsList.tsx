import React, { useState } from 'react';
import { ChartValues, IHistoricalDataRequest, IHistoricalDataResult } from '../../../model/HistoricalDataTypes';
import Loading from '../../Loading';
import axios, { AxiosInstance } from 'axios';
import BarAndLineChart from './BarAndLineChart';
import { useTranslation } from 'react-i18next';

interface IChartsListProps {
  requestData: IHistoricalDataRequest;
  reRenderForcedState: number;
}

const ChartsList: React.FC<IChartsListProps> = ({ requestData, reRenderForcedState }) => {
  const { t } = useTranslation();
  const shouldFetch = React.useRef(true);
  const [data, setData] = React.useState({} as IHistoricalDataResult);
  const [loading, setLoading] = useState<boolean>(true);
  const url = '/api/weather-measurements/historical';

  const blueColor = 'rgb(0, 128, 255)';
  const blueColorAlpha = 'rgba(0, 128, 255, 0.5)';
  const yellowColor = 'rgb(255, 178, 102)';
  const yellowColorAlpha = 'rgba(255, 178, 102, 0.5)';
  const redColor = 'rgb(204, 0, 0)';
  const redColorAlpha = 'rgba(204, 0, 0, 0.5)';

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

  // https://react-chartjs-2.js.org/examples/line-chart

  return (
    <>
      {loading ? (
        <Loading />
      ) : (
        <>
          <BarAndLineChart
            title={t('historical_data.chart.air_parameters')}
            chartType={requestData.chartView === ChartValues.Lines ? 'line' : 'bar'}
            chartTitle={t('historical_data.chart.air_parameters.air_pressure')}
            maxTitle={t('historical_data.chart.air_parameters.air_pressure.max')}
            avgTitle={t('historical_data.chart.air_parameters.air_pressure.avg')}
            minTitle={t('historical_data.chart.air_parameters.air_pressure.min')}
            maxBgColor={redColorAlpha}
            maxBorderColor={redColor}
            avgBgColor={yellowColorAlpha}
            avgBorderColor={yellowColor}
            minBgColor={blueColorAlpha}
            minBorderColor={blueColor}
            keys={data.airParameters.summaryByGroupingItem?.map((item) => item.key) as string[]}
            maxValues={data.airParameters.summaryByGroupingItem?.map((item) => item.maxPressure) as number[]}
            avgValues={data.airParameters.summaryByGroupingItem?.map((item) => item.avgPressure) as number[]}
            minValues={data.airParameters.summaryByGroupingItem?.map((item) => item.minPressure) as number[]}
          />
        </>
      )}
    </>
  );
};

export default ChartsList;
