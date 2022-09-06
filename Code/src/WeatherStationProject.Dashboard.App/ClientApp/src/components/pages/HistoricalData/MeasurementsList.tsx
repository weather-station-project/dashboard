import { IHistoricalDataRequest, IHistoricalDataResult } from '../../../model/HistoricalDataTypes';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import axios, { AxiosInstance } from 'axios';
import Loading from '../../Loading';
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';
import AirParametersTables from './AirParametersTables';
import { IAirParameters } from '../../../model/LastDataTypes';

interface IMeasurementsListProps {
  requestData: IHistoricalDataRequest;
  reRenderForcedState: number;
}

const MeasurementsList: React.FC<IMeasurementsListProps> = ({ requestData, reRenderForcedState }) => {
  const { t } = useTranslation();
  const shouldFetch = React.useRef(true);
  const [data, setData] = React.useState({} as IHistoricalDataResult);
  const [loading, setLoading] = useState<boolean>(true);
  const url = '/api/weather-measurements/historical';
  const sectionSeparation = { marginTop: 75 };

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
          includeSummary: false,
          includeMeasurements: true,
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

  // https://react-bootstrap-table.github.io/react-bootstrap-table2/

  return (
    <>
      {loading ? (
        <Loading />
      ) : (
        <>
          <h2>{t('historical_data.chart.air_parameters')}</h2>
          <AirParametersTables measurements={data.airParameters.measurements as IAirParameters[]} />
          <div className="mt-5"></div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.temperatures')}</h2>
          </div>
          <div className="mt-5"></div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.rainfall')}</h2>
          </div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.wind_measurements')}</h2>
          </div>
        </>
      )}
    </>
  );
};

export default MeasurementsList;
