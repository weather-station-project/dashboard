import React from "react";
import {useTranslation} from "react-i18next";

const Loading: React.FC = () => {
    const {t} = useTranslation();
    
    return (
        <div>{t("loading")}...</div>
    );
};

export default Loading;