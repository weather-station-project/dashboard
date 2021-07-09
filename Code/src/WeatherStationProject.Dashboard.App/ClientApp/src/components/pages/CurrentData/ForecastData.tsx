import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import Loading from "../../../Loading";
import axios from "axios";
import { IOpenWeatherApiResponse } from "../../../model/OpenWeatherApiTypes";
import { Carousel } from "react-bootstrap";
import CurrentDayData from "../../carousel/CurrentDayData";

interface IForecastDataProps {
    openWeatherApiKey: string;
}

interface ICoordinates {
    latitude: number;
    longitude: number;
}

const ForecastData: React.FC<IForecastDataProps> = ({ openWeatherApiKey }) => {
    const { t, i18n } = useTranslation();
    const [data, setData] = useState({} as IOpenWeatherApiResponse);
    const url = "https://api.openweathermap.org/data/2.5/onecall";

    useEffect(() => {
        function getCurrentLocation(): ICoordinates {
            const coordinates: ICoordinates = { latitude: 0, longitude: 0 };

            navigator.geolocation.getCurrentPosition(
                position => {
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
    }, [i18n.language]);

    return (
        <div>
            {data.hasOwnProperty("current") ?
                <Carousel>
                    <Carousel.Item>
                        <CurrentDayData data={data.current} />
                    </Carousel.Item>
                </Carousel>
                : <Loading />
            }
        </div>
    );
}

export default ForecastData;