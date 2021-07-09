import React from "react";
import { useTranslation } from "react-i18next";
import { Card } from "react-bootstrap";
import { IOpenWeatherApiCurrentDayData } from "../../model/OpenWeatherApiTypes";

interface ICurrentDayDataProps {
    data: IOpenWeatherApiCurrentDayData;
}

const CurrentDayData: React.FC<ICurrentDayDataProps> = ({ data }) => {
    const { t } = useTranslation();

    return (
        <Card style={{ width: "15rem" }}>
            <Card.Body>
                <Card.Title>{t("date.long", { date: new Date(data.dt * 1000) })}</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">{data.weather[0].description}</Card.Subtitle>
                <Card.Img variant="top" src="holder.js/100px180" />
                <Card.Text>
                    Some quick example text to build on the card title and make up the bulk of
                    the card's content.
                </Card.Text>
            </Card.Body>
        </Card>
    );
}

export default CurrentDayData;