import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { ListGroup } from "react-bootstrap";
import Loading from "../../../Loading";
import axios, { AxiosInstance } from "axios";
import { getAxiosRequestConfig } from "../../../consumers/AuthenticationApiHelper";
import { ILastData } from "../../../model/LastDataTypes";


interface ICurrentDataProps {
    weatherApiHost: string;
    authServiceHost: string;
    secret: string;
}

const CurrentData: React.FC<ICurrentDataProps> = ({ weatherApiHost, authServiceHost, secret }) => {
    const { t } = useTranslation();
    const [data, setData] = useState({} as ILastData);
    const url = "/api/v1/weather-measurements/last";

    useEffect(() => {
        async function fetchData() {
            const api: AxiosInstance = axios.create(await getAxiosRequestConfig(weatherApiHost, authServiceHost, secret));

            api.get<ILastData>(url)
                .then((response) => {
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
            {data.hasOwnProperty("airParameters") ?
                <ListGroup variant="flush">
                    <ListGroup.Item>
                        {t("current_data.last_data.air_parameters", {
                            pressure: data.airParameters.pressure,
                            humidity: data.airParameters.humidity,
                            dateTime: new Date(data.airParameters.dateTime)
                        })}
                    </ListGroup.Item>
                    <ListGroup.Item>{t("current_data.last_data.ambient_temperature", {
                        temperature: data.ambientTemperatures.temperature,
                        dateTime: new Date(data.ambientTemperatures.dateTime)
                    })}</ListGroup.Item>
                    <ListGroup.Item>{t("current_data.last_data.ground_temperature", {
                        temperature: data.groundTemperatures.temperature,
                        dateTime: new Date(data.groundTemperatures.dateTime)
                    })}</ListGroup.Item>
                    <ListGroup.Item>{t("current_data.last_data.rainfall", {
                        amount: data.rainfall.amount,
                        fromDateTime: new Date(data.rainfall.fromDateTime),
                        toDateTime: new Date(data.rainfall.toDateTime)
                    })}</ListGroup.Item>
                    <ListGroup.Item>{t("current_data.last_data.wind_measurement", {
                        speed: data.windMeasurements.speed,
                        direction: data.windMeasurements.direction,
                        dateTime: new Date(data.windMeasurements.dateTime)
                    })}</ListGroup.Item>
                    <ListGroup.Item>
                        {data.windMeasurementsGust.dateTime ?
                            t("current_data.last_data.wind_measurement_gust", {
                                speed: data.windMeasurementsGust.speed,
                                direction: data.windMeasurementsGust.direction,
                                dateTime: new Date(data.windMeasurementsGust.dateTime)
                            })
                            : t("current_data.last_data.wind_measurement_gust_not_found")}
                    </ListGroup.Item>
                </ListGroup>
                : <Loading />
            }
        </div>
    );
}

export default CurrentData;