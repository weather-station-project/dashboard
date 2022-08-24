import React, { useState } from 'react';
import { IHistoricalDataRequest, IHistoricalDataResult } from '../../../model/HistoricalDataTypes';
import Loading from '../../Loading';
import axios, { AxiosInstance } from 'axios';

interface IChartsListProps {
  requestData: IHistoricalDataRequest;
  reRenderForcedState: number;
}

const ChartsList: React.FC<IChartsListProps> = ({ requestData, reRenderForcedState }) => {
  const shouldFetch = React.useRef(true);
  const [data, setData] = React.useState({} as IHistoricalDataResult);
  const [loading, setLoading] = useState<boolean>(true);
  const url = '/api/weather-measurements/historical';

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
        <p>
          {data.airParameters.summaryByGroupingItem
            ? data.airParameters.summaryByGroupingItem[0].key.toString()
            : 'lala'}
        </p>
      )}
    </>
  );
};

export default ChartsList;
