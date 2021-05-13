import React from 'react';
import { useTranslation } from 'react-i18next';
import './LanguageSelector.css';

function LanguageSelector() {
    const { t, i18n } = useTranslation();
    const handleChange = (e) => {
        i18n.changeLanguage(e.target.value);
    };

    return (
        <select class="selectpicker" value={i18n.language} onChange={handleChange}>
            <option value="en">{t('navmenu.language_selector.english')}</option>
            <option value="es">{t('navmenu.language_selector.spanish')}</option>
        </select>
    );
}

export default LanguageSelector;