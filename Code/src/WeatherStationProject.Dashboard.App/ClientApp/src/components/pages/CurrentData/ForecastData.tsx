import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import Loading from "../../../Loading";
import axios from "axios";
import { ILastData } from "../../../model/OpenWeatherApiTypes";

interface IForecastDataProps {
    openWeatherApiKey: string;
}

const ForecastData: React.FC<IForecastDataProps> = ({ openWeatherApiKey }) => {
    const { t, i18n } = useTranslation();
    const [data, setData] = useState({} as ILastData);
    const url = "https://api.openweathermap.org/data/2.5/onecall";

    useEffect(() => {
        async function fetchData() {
            axios.get(url, {
                params: {
                    lat: 33,
                    lon: -94,
                    exclude: "minutely,hourly",
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
    }, []);

    return (
        <div>
            <h1>Coming soon!</h1>
        </div>
    );
}

export default ForecastData;