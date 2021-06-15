import React from "react";
import { useTranslation } from "react-i18next";

function languageSelector() {
    const { t, i18N } = useTranslation();
    const handleChange = (e) => {
        i18N.changeLanguage(e.target.value);
    };

    return (
        <select className="form-control form-control-sm" value={i18N.language} onChange={handleChange}>
            <option value="en">{t("navmenu.language_selector.english")}</option>
            <option value="es">{t("navmenu.language_selector.spanish")}</option>
        </select>
    );
}

export default languageSelector;