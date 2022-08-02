import React, {useEffect, useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Button, Card, ListGroup} from 'react-bootstrap';
import {IAccuWeatherDailyForecast} from '../../model/OpenWeatherApiTypes';
import {Link} from 'react-router-dom';

interface ICarouselDailyDataProps {
  data: IAccuWeatherDailyForecast;
}

const CarouselDailyData: React.FC<ICarouselDailyDataProps> = ({data}) => {
  const {t} = useTranslation();
  const [showDay, setShowDay] = useState(true);
  const [dayNightData, setDayNightData] = useState(data.Day);

  useEffect(() => {
    if (showDay) {
      setDayNightData(data.Day);
    } else {
      setDayNightData(data.Night);
    }
  }, [showDay, data.Day, data.Night]);

  return (
      <Card bg="light" style={{width: '17rem'}}>
        <Card.Body>
          {showDay ? (
              <Button data-testid="button-night" variant="outline-secondary" onClick={() => setShowDay(false)}>
                {t('current_data.forecast_data.night')}
              </Button>
          ) : (
              <Button data-testid="button-day" variant="outline-primary" onClick={() => setShowDay(true)}>
                {t('current_data.forecast_data.day')}
              </Button>
          )}
          <br/>
          <Card.Title>{t('date.date', {date: new Date(data.EpochDate * 1000)})}</Card.Title>
          <Card.Img
              variant="top"
              src={
                'https://developer.accuweather.com/sites/default/files/' +
                dayNightData.Icon.toString().padStart(2, '0') +
                '-s.png'
              }
          />
          <Card.Subtitle className="mb-2 text-muted">{dayNightData.LongPhrase}</Card.Subtitle>
          <br/>
          <ListGroup variant="flush">
            <ListGroup.Item variant="light">
              {showDay
                  ? t('current_data.forecast_data.temperature_day', {temperature: data.Temperature.Maximum.Value})
                  : t('current_data.forecast_data.temperature_night', {temperature: data.Temperature.Minimum.Value})}
            </ListGroup.Item>
            <ListGroup.Item variant="light">
              {t('current_data.forecast_data.wind', {
              speed: dayNightData.Wind.Speed.Value,
              direction: dayNightData.Wind.Direction.Localized,
            })}
          </ListGroup.Item>
          <ListGroup.Item variant="light">
            {t('current_data.forecast_data.wind_gust_day', {
              speed: dayNightData.WindGust.Speed.Value,
              direction: dayNightData.WindGust.Direction.Localized,
            })}
          </ListGroup.Item>
          <ListGroup.Item variant="light">
            {t('current_data.forecast_data.rain_day', {amount: dayNightData.Rain.Value})}
          </ListGroup.Item>
          <ListGroup.Item variant="light">
            <Link to={{ pathname: data.Link }} target="_blank">
              Link
            </Link>
          </ListGroup.Item>
        </ListGroup>
      </Card.Body>
    </Card>
  );
};

export default CarouselDailyData;
