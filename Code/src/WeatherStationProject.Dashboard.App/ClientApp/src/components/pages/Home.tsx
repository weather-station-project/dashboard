import React from "react";
import { useTranslation } from "react-i18next";

const Home: React.FC = () => {
    const { t } = useTranslation();

    return (
        <div style={{ paddingTop: 20 }}>
            <h1>Weather Station Project - Dashboard</h1>
            <p>{t("home.introduction")}</p>
            <p>{t("home.more_information")}<a href="https://github.com/weather-station-project/weather-station-project" target="blank">link</a>.</p>
            <p>{t("home.technologies")}

            </p>
        </div>
    );
}

export default Home;