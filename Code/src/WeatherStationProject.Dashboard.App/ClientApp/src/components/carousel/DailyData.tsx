import React from "react";
import { useTranslation } from "react-i18next";
import { Card, ListGroup } from "react-bootstrap";
import { IOpenWeatherApiDailyData } from "../../model/OpenWeatherApiTypes";

interface IDailyDataProps {
    data: IOpenWeatherApiDailyData;
}

const DailyData: React.FC<IDailyDataProps> = ({ data }) => {
    const { t } = useTranslation();

    return (
        <Card bg="light" style={{ width: "18rem" }}>
            <Card.Body>
                <Card.Title>{t("date.date", { date: new Date(data.dt * 1000) })}</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">{data.weather[0].description}</Card.Subtitle>
                <Card.Img variant="bottom" src={"http://openweathermap.org/img/wn/" + data.weather[0].icon + "@2x.png"} />
                <ListGroup variant="flush">
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.temperature_day", { temperature: data.temp })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.pressure", { pressure: data.pressure })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.humidity", { humidity: data.humidity })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.uv", { uv: data.uvi })}
                    </ListGroup.Item>
                    <ListGroup.Item variant="light">
                        {t("current_data.forecast_data.wind_speed", { windSpeed: data.wind_speed })}
                    </ListGroup.Item>
                    {data.hasOwnProperty("wind_gust") &&
                        <ListGroup.Item variant="light">
                            {t("current_data.forecast_data.wind_gust", { windSpeed: data.wind_gust })}
                        </ListGroup.Item>
                    }
                    {data.hasOwnProperty("rain") &&
                        <ListGroup.Item variant="light">
                            {t("current_data.forecast_data.rain_day", { amount: data.rain })}
                        </ListGroup.Item>
                    }
                </ListGroup>
            </Card.Body>
        </Card>
    );
}

export default DailyData;