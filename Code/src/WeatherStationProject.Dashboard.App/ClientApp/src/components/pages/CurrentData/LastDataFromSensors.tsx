import React from 'react';
import { useTranslation } from 'react-i18next';
import { ListGroup } from 'react-bootstrap';
import axios, { AxiosInstance } from 'axios';
import { ILastData } from '../../../model/LastDataTypes';
import Loading from '../../Loading';

const LastDataFromSensors: React.FC = () => {
  const { t } = useTranslation();
  const [data, setData] = React.useState({} as ILastData);
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
        });
    }

    fetchData();
  }, []);

  return (
    <div>
      {Object.prototype.hasOwnProperty.call(data, 'airParameters') ? (
        <ListGroup variant="flush">
          <ListGroup.Item>
            {t('current_data.last_data.air_parameters', {
              pressure: data.airParameters.pressure,
              humidity: data.airParameters.humidity,
              dateTime: new Date(data.airParameters.dateTime),
            })}
          </ListGroup.Item>
          <ListGroup.Item>
            {t('current_data.last_data.ambient_temperature', {
              temperature: data.ambientTemperatures.temperature,
              dateTime: new Date(data.ambientTemperatures.dateTime),
            })}
          </ListGroup.Item>
          <ListGroup.Item>
            {t('current_data.last_data.ground_temperature', {
              temperature: data.groundTemperatures.temperature,
              dateTime: new Date(data.groundTemperatures.dateTime),
            })}
          </ListGroup.Item>
          <ListGroup.Item>
            {t('current_data.last_data.rainfall', {
              amount: data.rainfallDuringTime.amount,
              fromDateTime: new Date(data.rainfallDuringTime.fromDateTime),
              toDateTime: new Date(data.rainfallDuringTime.toDateTime),
            })}
          </ListGroup.Item>
          <ListGroup.Item>
            {t('current_data.last_data.wind_measurement', {
              speed: data.windMeasurements.speed,
              direction: data.windMeasurements.direction,
              dateTime: new Date(data.windMeasurements.dateTime),
            })}
          </ListGroup.Item>
          <ListGroup.Item>
            {data.windMeasurementsGust.dateTime
              ? t('current_data.last_data.wind_measurement_gust', {
                  speed: data.windMeasurementsGust.speed,
                  direction: data.windMeasurementsGust.direction,
                  dateTime: new Date(data.windMeasurementsGust.dateTime),
                })
              : t('current_data.last_data.wind_measurement_gust_not_found')}
          </ListGroup.Item>
        </ListGroup>
      ) : (
        <Loading />
      )}
    </div>
  );
};

export default LastDataFromSensors;
