import React from "react";
import { useTranslation } from "react-i18next";
import { Card, ListGroup } from "react-bootstrap";
import { IAccuWeatherCurrentConditionsResponse } from "../../model/OpenWeatherApiTypes";
import { Link } from "react-router-dom";

interface ICarouselCurrentDataProps {
    data: IAccuWeatherCurrentConditionsResponse;
}

const CarouselCurrentData: React.FC<ICarouselCurrentDataProps> = ({ data }) => {
    const { t } = useTranslation();

    return (
        <Card bg="light" style={{ width: "18rem" }}>
            <Card.Body>
                <Card.Title>{t("date.now") + ", " + t("date.long", { date: new Date(data.EpochTime * 1000) })}</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">{data.WeatherText}</Card.Subtitle>
                <Card.Img variant="bottom" src={"https://developer.accuweather.com/sites/default/files/" + data.WeatherIcon + "-s.png"} />
                <ListGroup variant="flush">
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.temperature", { temperature: data.Temperature.Metric.Value })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.pressure", { pressure: data.Pressure.Metric.Value })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.humidity", { humidity: data.RelativeHumidity })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.uv", { uv: data.UVIndexText })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.wind", { speed: data.Wind.Speed, direction: data.Wind.Direction })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.wind_gust", { windSpeed: data.WindGust.Speed })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.rain", { amount: data.Precip1hr.Metric.Value })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        <Link to={data.Link}></Link>
                    </ListGroup.Item>
                </ListGroup>
            </Card.Body>
        </Card>
    );
}

export default CarouselCurrentData;