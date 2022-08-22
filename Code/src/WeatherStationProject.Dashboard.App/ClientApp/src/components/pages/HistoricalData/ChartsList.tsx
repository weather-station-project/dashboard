import React, { useState } from 'react';
import { IHistoricalDataRequest } from '../../../model/HistoricalDataTypes';
import Loading from '../../Loading';
import axios, { AxiosInstance } from 'axios';
import { ILastData } from '../../../model/LastDataTypes';

interface IChartsListProps {
  requestData: IHistoricalDataRequest;
}

const ChartsList: React.FC<IChartsListProps> = ({ requestData }) => {
  const [loading, setLoading] = useState<boolean>(true);
  const url = '/api/weather-measurements/last';

  React.useEffect(() => {
    async function fetchData() {
      const api: AxiosInstance = axios.create({
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        method: 'GET',
        baseURL: '/',
      });

      api
        .get<string>(url)
        .then((response) => {
          setData(JSON.parse(response.data) as ILastData);
        })
        .catch((e) => {
          setData((() => {
            throw e;
          }) as never);
        })
        .finally(() => setLoading(false));
    }

    fetchData();
  }, []);

  return <>{loading ? <Loading /> : <p>aa</p>}</>;
};

export default ChartsList;
