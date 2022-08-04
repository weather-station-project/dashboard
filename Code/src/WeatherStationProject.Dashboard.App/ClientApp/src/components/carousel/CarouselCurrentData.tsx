import React from 'react';
import {useTranslation} from 'react-i18next';
import {Card, ListGroup} from 'react-bootstrap';
import {IAccuWeatherCurrentConditionsResponse} from '../../model/OpenWeatherApiTypes';
import {Link} from 'react-router-dom';

interface ICarouselCurrentDataProps {
  data: IAccuWeatherCurrentConditionsResponse;
}

const CarouselCurrentData: React.FC<ICarouselCurrentDataProps> = ({data}) => {
  const {t} = useTranslation();

  return (
      <Card bg="light" style={{width: '17rem'}}>
        <Card.Body>
          <Card.Title>{t('date.now') + ', ' + t('date.long', {date: new Date(data.EpochTime * 1000)})}</Card.Title>
          <Card.Img
              variant="top"
              src={
                'https://developer.accuweather.com/sites/default/files/' +
                data.WeatherIcon.toString().padStart(2, '0') +
                '-s.png'
              }
          />
          <Card.Subtitle className="mb-2 text-muted">{data.WeatherText}</Card.Subtitle>
          <br/>
          <ListGroup variant="flush">
            <ListGroup.Item variant="light">
              {t('current_data.forecast_data.temperature', {temperature: data.Temperature.Metric.Value})}
            </ListGroup.Item>
            <ListGroup.Item variant="light">
              {t('current_data.forecast_data.pressure', {pressure: data.Pressure.Metric.Value})}
            </ListGroup.Item>
            <ListGroup.Item variant="light">
              {t('current_data.forecast_data.humidity', {humidity: data.RelativeHumidity})}
            </ListGroup.Item>
            <ListGroup.Item variant="light">
              {t('current_data.forecast_data.uv', {uv: data.UVIndexText})}
            </ListGroup.Item>
            <ListGroup.Item variant="light">
              {t('current_data.forecast_data.wind', {
                speed: data.Wind.Speed.Metric.Value,
                direction: data.Wind.Direction.Localized,
              })}
            </ListGroup.Item>
            <ListGroup.Item variant="light">
              {t('current_data.forecast_data.wind_gust', {speed: data.WindGust.Speed.Metric.Value})}
            </ListGroup.Item>
            <ListGroup.Item variant="light">
              {t('current_data.forecast_data.rain', {amount: data.Precip1hr.Metric.Value})}
            </ListGroup.Item>
            <ListGroup.Item variant="light">
              <Link to={{pathname: data.Link}} target="_blank">
                Link
              </Link>
          </ListGroup.Item>
        </ListGroup>
      </Card.Body>
    </Card>
  );
};

export default CarouselCurrentData;
