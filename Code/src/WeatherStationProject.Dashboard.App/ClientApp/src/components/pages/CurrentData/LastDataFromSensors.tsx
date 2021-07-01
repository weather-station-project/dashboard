import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { ListGroup, Button } from "react-bootstrap";
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
                .then((response) => setData(response.data))
                .catch(e => {
                    setData((() => { throw e }) as any);
                });
        };

        fetchData();
    }, []);

    return (
        <div>
            {data.hasOwnProperty("air-parameters") ?
                <ListGroup>
                    <ListGroup.Item>{t("current_data.last_data.ambient_temperature", {
                        temperature: data["air-parameters"].pressure,
                        dateTime: new Date(data["air-parameters"].dateTime)
                    })}</ListGroup.Item>
                    <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
                    <ListGroup.Item>Morbi leo risus</ListGroup.Item>
                    <ListGroup.Item>Porta ac consectetur ac</ListGroup.Item>
                    <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
                </ListGroup>
                : <Loading />
            }
        </div>);
}

export default CurrentData;