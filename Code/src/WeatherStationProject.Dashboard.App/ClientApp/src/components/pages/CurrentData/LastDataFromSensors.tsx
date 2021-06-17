import React, { Component } from "react";
import { useTranslation } from "react-i18next";
import { ListGroup } from "react-bootstrap";
import Loading from "../../../Loading";
import { WeatherStationDataApi } from "../../../consumers/WeatherStationDataApi";

interface ICurrentDataProps {
    weatherApiHost: string;
    authServiceHost: string;
    secret: string;
}

interface ICurrentDataState {
    data: string;
}

export default class CurrentData extends Component<ICurrentDataProps, ICurrentDataState> {
    api: WeatherStationDataApi;

    constructor(props: ICurrentDataProps) {
        super(props);

        this.api = new WeatherStationDataApi(props.weatherApiHost, props.authServiceHost, props.secret);

        this.setState({
            data: "lala " + this.api.apiHost
        });
    }

    render() {
        return <p>{this.state.data}</p>
    }
}