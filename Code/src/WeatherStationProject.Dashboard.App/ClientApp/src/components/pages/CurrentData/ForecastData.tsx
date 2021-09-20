import React, {useEffect, useState} from "react";
import {useTranslation} from "react-i18next";
import Loading from "../../../Loading";
import axios from "axios";
import {
    IAccuWeatherCurrentConditionsResponse,
    IAccuWeatherForecastResponse,
    IAccuWeatherLocationSearchResponse
} from "../../../model/OpenWeatherApiTypes";
import Carousel from "react-multi-carousel";
import "react-multi-carousel/lib/styles.css";
import CarouselCurrentData from "../../carousel/CarouselCurrentData";
import CarouselDailyData from "../../carousel/CarouselDailyData";

interface IForecastDataProps {
    weatherApiKey: string;
    cityName: string;
}

const ForecastData: React.FC<IForecastDataProps> = ({weatherApiKey, cityName}) => {
    const {i18n} = useTranslation();
    const [currentData, setCurrentData] = useState({} as IAccuWeatherCurrentConditionsResponse);
    const [forecastData, setForecastData] = useState({} as IAccuWeatherForecastResponse);
    const responsive = {
        desktop: {
            breakpoint: {max: 3000, min: 1024},
            items: 3,
            slidesToSlide: 3
        },
        tablet: {
            breakpoint: {max: 1024, min: 464},
            items: 2,
            slidesToSlide: 2
        },
        mobile: {
            breakpoint: {max: 464, min: 0},
            items: 1,
            slidesToSlide: 1
        }
    };

    useEffect(() => {
        async function fetchData() {
            const locationKey = await getLocationKeyByCityName();
            if (locationKey !== undefined) {
                await Promise.all([fetchCurrentData(locationKey), fetchForecastData(locationKey)]);
            }
        }

        async function getLocationKeyByCityName(): Promise<string | undefined> {
            try {
                const response = await axios.get<IAccuWeatherLocationSearchResponse[]>("https://dataservice.accuweather.com/locations/v1/search",
                    {
                        params: {
                            apikey: weatherApiKey,
                            q: cityName,
                            details: false,
                            language: i18n.language
                        }
                    });

                console.debug(response);
                return response.data[0].Key;
            } catch (e) {
                setCurrentData((() => {
                    throw e
                }) as any);
            }
        }

        async function fetchCurrentData(locationKey: string) {
            axios.get<IAccuWeatherCurrentConditionsResponse[]>("https://dataservice.accuweather.com/currentconditions/v1/" + locationKey, {
                params: {
                    apikey: weatherApiKey,
                    details: true,
                    language: i18n.language
                }
            }).then((response) => {
                console.debug(response);
                setCurrentData(response.data[0]);
            }).catch(e => {
                setCurrentData((() => {
                    throw e
                }) as any);
            });
        }

        async function fetchForecastData(locationKey: string) {
            axios.get<IAccuWeatherForecastResponse>("https://dataservice.accuweather.com/forecasts/v1/daily/5day/" + locationKey, {
                params: {
                    apikey: weatherApiKey,
                    language: i18n.language,
                    details: true,
                    metric: true
                }
            }).then((response) => {
                console.debug(response);
                setForecastData(response.data);
            }).catch(e => {
                setForecastData((() => {
                    throw e
                }) as any);
            });
        }

        fetchData();
    }, [i18n.language, weatherApiKey, cityName]);

    return (
        <div>
            {
                currentData.hasOwnProperty("EpochTime") && forecastData.hasOwnProperty("DailyForecasts") ?
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
                        removeArrowOnDeviceType={["tablet", "mobile"]}
                        dotListClass="custom-dot-list-style"
                    >
                        <div><CarouselCurrentData data={currentData}/></div>
                        {forecastData.DailyForecasts.map((dayData, idx) => <div key="idx"><CarouselDailyData
                            data={dayData}/></div>)}
                    </Carousel>
                    : <Loading/>
            }
        </div>
    );
}

export default ForecastData;
