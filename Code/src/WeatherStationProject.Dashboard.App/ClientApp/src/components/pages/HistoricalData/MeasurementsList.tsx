import { IHistoricalDataRequest, IHistoricalDataResult } from '../../../model/HistoricalDataTypes';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import axios, { AxiosInstance } from 'axios';
import Loading from '../../Loading';
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';
import { IAirParameters, IAmbientTemperatures, IRainfall, IWindMeasurements } from '../../../model/LastDataTypes';
import { getColumn, getDateTimeColumn } from './Tables/BaseConfig';
import MeasurementsTable from './Tables/MeasurementsTable';

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

  return (
    <>
      {loading ? (
        <Loading />
      ) : (
        <>
          <h2>{t('historical_data.chart.air_parameters')}</h2>
          <MeasurementsTable
            measurements={data.airParameters.measurements as IAirParameters[]}
            columns={[
              getDateTimeColumn(t),
              getColumn('pressure', t('historical_data.chart.air_parameters.air_pressure'), '#c8e6c9'),
              getColumn('humidity', t('historical_data.chart.air_parameters.humidity'), '#c8e6c9'),
            ]}
            columnNameSort={'dateTime'}
          />
          <div className="mt-5"></div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.temperatures')}</h2>
            <MeasurementsTable
              measurements={data.ambientTemperatures.measurements as IAmbientTemperatures[]}
              columns={[
                getDateTimeColumn(t),
                getColumn('temperature', t('historical_data.chart.temperatures.ambient'), '#c8e6c9'),
              ]}
              columnNameSort={'dateTime'}
            />
            <MeasurementsTable
              measurements={data.groundTemperatures.measurements as IAmbientTemperatures[]}
              columns={[
                getDateTimeColumn(t),
                getColumn('temperature', t('historical_data.chart.temperatures.ground'), '#c8e6c9'),
              ]}
              columnNameSort={'dateTime'}
            />
          </div>
          <div className="mt-5"></div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.rainfall')}</h2>
            <MeasurementsTable
              measurements={data.rainfall.measurements as IRainfall[]}
              columns={[
                getDateTimeColumn(t, 'fromDateTime'),
                getDateTimeColumn(t, 'toDateTime'),
                getColumn('amount', t('historical_data.chart.rainfall.chart'), '#c8e6c9'),
              ]}
              columnNameSort={'fromDateTime'}
            />
          </div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.wind_measurements')}</h2>
            <MeasurementsTable
              measurements={data.windMeasurements.measurements as IWindMeasurements[]}
              columns={[
                getDateTimeColumn(t),
                getColumn('speed', t('historical_data.chart.wind_measurements.speed'), '#c8e6c9'),
                getColumn('direction', t('historical_data.chart.wind_measurements.direction'), '#c8e6c9'),
              ]}
              columnNameSort={'dateTime'}
            />
          </div>
        </>
      )}
    </>
  );
};

export default MeasurementsList;
