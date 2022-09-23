import { IHistoricalDataRequest, IHistoricalDataResult } from '../../../model/HistoricalDataTypes';
import React from 'react';
import { useTranslation } from 'react-i18next';
import axios, { AxiosInstance } from 'axios';
import Loading from '../../Loading';
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';
import { IAirParameters, IAmbientTemperatures, IRainfall, IWindMeasurements } from '../../../model/LastDataTypes';
import { getColumn, getDateTimeColumn } from './Tables/BaseConfig';
import MeasurementsTable from './Tables/MeasurementsTable';
import {
  blueColorAlpha,
  darkGreyColor,
  greenColorAlpha,
  greyColor,
  redColorAlpha,
  yellowColor,
} from './ChartsAndListsConstants';

interface IMeasurementsListProps {
  requestData: IHistoricalDataRequest;
  reRenderForcedState: number;
}

const MeasurementsList: React.FC<IMeasurementsListProps> = ({ requestData, reRenderForcedState }) => {
  const { t } = useTranslation();
  const shouldFetch = React.useRef(true);
  const [data, setData] = React.useState({} as IHistoricalDataResult);
  const [loading, setLoading] = React.useState<boolean>(true);
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
              getColumn('pressure', t('historical_data.chart.air_parameters.air_pressure'), greyColor),
              getColumn('humidity', t('historical_data.chart.air_parameters.humidity'), blueColorAlpha),
            ]}
            columnNameSort={'dateTime'}
            csvFilename="air_parameters"
          />
          <div className="mt-5"></div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.temperatures')}</h2>
            <MeasurementsTable
              measurements={data.ambientTemperatures.measurements as IAmbientTemperatures[]}
              columns={[
                getDateTimeColumn(t),
                getColumn('temperature', t('historical_data.chart.temperatures.ambient'), redColorAlpha),
              ]}
              columnNameSort={'dateTime'}
              csvFilename="ambient_temperatures"
            />
            <MeasurementsTable
              measurements={data.groundTemperatures.measurements as IAmbientTemperatures[]}
              columns={[
                getDateTimeColumn(t),
                getColumn('temperature', t('historical_data.chart.temperatures.ground'), yellowColor),
              ]}
              columnNameSort={'dateTime'}
              csvFilename="ground_temperatures"
            />
          </div>
          <div className="mt-5"></div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.rainfall')}</h2>
            <MeasurementsTable
              measurements={data.rainfall.measurements as IRainfall[]}
              columns={[
                getDateTimeColumn(t, 'fromDateTime'),
                getColumn('amount', t('historical_data.chart.rainfall.chart'), blueColorAlpha),
              ]}
              columnNameSort={'fromDateTime'}
              csvFilename="rainfall_measurements"
            />
          </div>
          <div style={sectionSeparation}>
            <h2>{t('historical_data.chart.wind_measurements')}</h2>
            <MeasurementsTable
              measurements={data.windMeasurements.measurements as IWindMeasurements[]}
              columns={[
                getDateTimeColumn(t),
                getColumn('speed', t('historical_data.chart.wind_measurements.speed'), greenColorAlpha),
                getColumn('direction', t('historical_data.chart.wind_measurements.direction'), darkGreyColor),
              ]}
              columnNameSort={'dateTime'}
              csvFilename="wind_measurements"
            />
          </div>
        </>
      )}
    </>
  );
};

export default MeasurementsList;
