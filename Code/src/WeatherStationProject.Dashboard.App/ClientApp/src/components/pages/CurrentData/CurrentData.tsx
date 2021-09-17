import React from "react";
import {useTranslation} from "react-i18next";
import LastDataFromSensors from "./LastDataFromSensors";
import ForecastData from "./ForecastData";
import {ErrorBoundary} from "../../../ErrorBoundary";


const CurrentData: React.FC = () => {
    const {t} = useTranslation();

    return (
        <div style={{paddingTop: 20}}>
            <h1>{t("current_data.last_data_title")}</h1>
            <ErrorBoundary>
                <LastDataFromSensors
                    weatherApiHost={(process.env.REACT_APP_WEATHER_API_HOST as string)}
                    authServiceHost={(process.env.REACT_APP_AUTHENTICATION_SERVICE_HOST as string)}
                    secret={(process.env.REACT_APP_AUTHENTICATION_SECRET as string)}/>
            </ErrorBoundary>
            <br/>
            <h1>{t("current_data.forecast_title")}</h1>
            <ErrorBoundary>
                <ForecastData weatherApiKey={(process.env.REACT_APP_ACCU_WEATHER_API_KEY as string)}
                              cityName={(process.env.REACT_APP_ACCUWEATHER_LOCATION_NAME as string)}/>
            </ErrorBoundary>
        </div>
    );
}

export default CurrentData;