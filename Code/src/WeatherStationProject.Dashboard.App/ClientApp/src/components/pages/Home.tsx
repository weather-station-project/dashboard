import React from "react";
import { ListGroup } from "react-bootstrap";
import { useTranslation } from "react-i18next";

const Home: React.FC = () => {
  const { t } = useTranslation();

  return (
    <div style={{ paddingTop: 20 }}>
      <h1>Weather Station Project - Dashboard</h1>
      <p>{t("home.introduction")}.</p>
      <p>
        {t("home.more_information")}
        <a href="https://github.com/weather-station-project/weather-station-project" target="blank">
          link
        </a>
        .
      </p>
      <p>{t("home.technologies")}:</p>
      <ListGroup variant="flush">
        <ListGroup.Item>{t("home.technologies.clientapp")}.</ListGroup.Item>
        <ListGroup.Item>{t("home.technologies.accuweather")}.</ListGroup.Item>
        <ListGroup.Item>{t("home.technologies.apis")}.</ListGroup.Item>
        <ListGroup.Item>{t("home.technologies.authentication")}.</ListGroup.Item>
        <ListGroup.Item>{t("home.technologies.gateway")}.</ListGroup.Item>
        <ListGroup.Item>{t("home.technologies.docker")}.</ListGroup.Item>
      </ListGroup>
    </div>
  );
};

export default Home;
