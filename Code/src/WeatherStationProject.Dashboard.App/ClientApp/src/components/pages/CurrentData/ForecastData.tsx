import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import Loading from "../../../Loading";
import axios from "axios";
import { IOpenWeatherApiResponse } from "../../../model/OpenWeatherApiTypes";
import Carousel from "react-multi-carousel";
import "react-multi-carousel/lib/styles.css";
import CarouselCurrentData from "../../carousel/CarouselCurrentData";
import CarouselDailyData from "../../carousel/CarouselDailyData";

interface IForecastDataProps {
    openWeatherApiKey: string;
}

interface ICoordinates {
    latitude: number;
    longitude: number;
}

const ForecastData: React.FC<IForecastDataProps> = ({ openWeatherApiKey }) => {
    const { i18n } = useTranslation();
    const [data, setData] = useState({} as IOpenWeatherApiResponse);
    const url = "https://api.openweathermap.org/data/2.5/onecall";
    const responsive = {
        desktop: {
            breakpoint: { max: 3000, min: 1024 },
            items: 3,
            slidesToSlide: 3
        },
        tablet: {
            breakpoint: { max: 1024, min: 464 },
            items: 2,
            slidesToSlide: 2
        },
        mobile: {
            breakpoint: { max: 464, min: 0 },
            items: 1,
            slidesToSlide: 1
        }
    };

    useEffect(() => {
        function getCurrentLocation(): ICoordinates {
            const coordinates: ICoordinates = { latitude: 0, longitude: 0 };

            navigator.geolocation.getCurrentPosition(
                position => {
                    console.debug(position);

                    coordinates.latitude = position.coords.latitude;
                    coordinates.longitude = position.coords.longitude;
                },
                error => {
                    setData((() => { throw error }) as any);
                }
            );

            return coordinates;
        }

        async function fetchData() {
            const coordinates = getCurrentLocation();

            axios.get<IOpenWeatherApiResponse>(url, {
                params: {
                    lat: coordinates.latitude,
                    lon: coordinates.longitude,
                    exclude: "minutely,hourly,alerts",
                    appid: openWeatherApiKey,
                    units: "metric",
                    lang: i18n.language
                }
            }).then((response) => {
                console.debug(response);
                setData(response.data);
            }).catch(e => {
                setData((() => { throw e }) as any);
            });
        };

        fetchData();
    }, [i18n.language, openWeatherApiKey]);

    return (
        <div>
            {data.hasOwnProperty("current") ?
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
                    <div><CarouselCurrentData data={data.current} /></div>
                    {data.daily.map((dayData, idx) => <div key="idx"><CarouselDailyData data={dayData} /></div>)}
                </Carousel>
                : <Loading />
            }
        </div>
    );
}

export default ForecastData;