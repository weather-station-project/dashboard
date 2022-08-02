import React from 'react';
import {useTranslation} from 'react-i18next';
import axios from 'axios';
import {
  IAccuWeatherCurrentConditionsResponse,
  IAccuWeatherForecastResponse,
  IAccuWeatherLocationSearchResponse,
} from '../../../model/OpenWeatherApiTypes';
import Carousel from 'react-multi-carousel';
import 'react-multi-carousel/lib/styles.css';
import CarouselCurrentData from '../../carousel/CarouselCurrentData';
import CarouselDailyData from '../../carousel/CarouselDailyData';
import {Button, OverlayTrigger, Tooltip} from 'react-bootstrap';

const ForecastData: React.FC = () => {
  const {t, i18n} = useTranslation();
  const [currentData, setCurrentData] = React.useState({} as IAccuWeatherCurrentConditionsResponse);
  const [forecastData, setForecastData] = React.useState({} as IAccuWeatherForecastResponse);
  const [retrieveDataFromAccuWeather, setRetrieveDataFromAccuWeather] = React.useState(false);
  const responsive = {
    desktop: {
      breakpoint: {max: 3000, min: 1024},
      items: 3,
      slidesToSlide: 3,
    },
    tablet: {
      breakpoint: { max: 1024, min: 464 },
      items: 2,
      slidesToSlide: 2,
    },
    mobile: {
      breakpoint: { max: 464, min: 0 },
      items: 1,
      slidesToSlide: 1,
    },
  };

  React.useEffect(() => {
    async function fetchData() {
      const locationKey = await getLocationKeyByCityName();
      if (locationKey !== undefined) {
        await Promise.all([fetchCurrentData(locationKey), fetchForecastData(locationKey)]);
      }
    }

    async function getLocationKeyByCityName(): Promise<string | undefined> {
      try {
        const response = await axios.get(`/api/accu-weather/location-key/${i18n.language}`);
        const parsedData = response.data[0] as IAccuWeatherLocationSearchResponse;

        return parsedData.Key;
      } catch (e) {
        setCurrentData((() => {
          throw e;
        }) as never);
      }
    }

    async function fetchCurrentData(locationKey: string) {
      axios
        .get(`/api/accu-weather/current-conditions/${locationKey}/${i18n.language}`)
        .then((response) => {
          setCurrentData(response.data[0] as IAccuWeatherCurrentConditionsResponse);
        })
        .catch((e) => {
          setCurrentData((() => {
            throw e;
          }) as never);
        });
    }

    async function fetchForecastData(locationKey: string) {
      axios
        .get(`/api/accu-weather/forecast-data/${locationKey}/${i18n.language}`)
        .then((response) => {
          setForecastData(response.data as IAccuWeatherForecastResponse);
        })
        .catch((e) => {
          setForecastData((() => {
            throw e;
          }) as never);
        });
    }

    if (retrieveDataFromAccuWeather) {
      fetchData();
    }
  }, [retrieveDataFromAccuWeather, i18n.language]);

  return (
      <>
        {Object.prototype.hasOwnProperty.call(currentData, 'EpochTime') &&
        Object.prototype.hasOwnProperty.call(forecastData, 'DailyForecasts') ? (
            <>
              <div data-testid="carousel-id"/>
              <Carousel
                  swipeable={true}
                  draggable={false}
                  showDots={true}
                  responsive={responsive}
                  ssr={true}
                  infinite={true}
                  autoPlay={false}
                  keyBoardControl={true}
                  customTransition="all .5"
                  transitionDuration={500}
                  containerClass="carousel-container"
                  removeArrowOnDeviceType={['tablet', 'mobile']}
                  dotListClass="custom-dot-list-style"
          >
            <div>
              <CarouselCurrentData data={currentData} />
            </div>
            {forecastData.DailyForecasts.map((dayData, idx) => (
              <div key={idx}>
                <CarouselDailyData data={dayData} />
              </div>
            ))}
          </Carousel>
        </>
      ) : (
        <>
          <OverlayTrigger
              key="right"
              placement="right"
              overlay={<Tooltip id="tooltip-right">{t('current_data.retrieve_data_from_accuweather_alert')}</Tooltip>}
          >
            <Button data-testid="button-id" variant="outline-info" onClick={() => setRetrieveDataFromAccuWeather(true)}>
              {t('current_data.retrieve_data_from_accuweather_button')}
            </Button>
          </OverlayTrigger>
        </>
      )}
    </>
  );
};

export default ForecastData;
