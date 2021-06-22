import React, { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { ListGroup } from "react-bootstrap";
import Loading from "../../../Loading";
import axios from "axios";
import { getAxiosInterceptor } from "../../../consumers/AuthenticationApiHelper";

interface ICurrentDataProps {
    weatherApiHost: string;
    authServiceHost: string;
    secret: string;
}

interface ITemperature {
    dateTime: Date;
    temperature: Number;
}

const useAsyncError = () => {
    const [_, setError] = React.useState();
    return React.useCallback(
        e => {
            setError(() => {
                throw e;
            });
        },
        [setError],
    );
};

const CurrentData: React.FC<ICurrentDataProps> = ({ weatherApiHost, authServiceHost, secret }) => {
    const { t } = useTranslation();
    const [data, setData] = useState({} as ITemperature);
    const [error2, setError] = useState(null);
    const url = "/api/v1/AmbientTemperatures/last";

    useEffect(() => {
        async function fetchData() {
            axios.interceptors.request.use(() => getAxiosInterceptor(authServiceHost, secret), error => Promise.reject(error));

            axios.get<ITemperature>(weatherApiHost + url).then((response) => setData(response.data)).catch(e => {
                throw e;
            });
        };

        fetchData();
    }, []);

    return (
        <div>
            {/*{data !== null ?
                <ListGroup>
                    <ListGroup.Item>{t("current_data.last_data.ambient_temperature", {
                        temperature: data.temperature,
                        dateTime: new Date(data.dateTime)
                    })}</ListGroup.Item>
                    <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
                    <ListGroup.Item>Morbi leo risus</ListGroup.Item>
                    <ListGroup.Item>Porta ac consectetur ac</ListGroup.Item>
                    <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
                </ListGroup>
                : <Loading />
            }*/}
        </div>);
}

export default CurrentData;