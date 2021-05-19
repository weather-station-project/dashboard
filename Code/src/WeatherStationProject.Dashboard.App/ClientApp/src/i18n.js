import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import moment from 'moment';
import Backend from 'i18next-http-backend';
import LanguageDetector from 'i18next-browser-languagedetector';

i18n
    .use(Backend)
    .use(LanguageDetector)
    .use(initReactI18next)
    .init({
        fallbackLng: 'en',
        debug: false,

        interpolation: {
            escapeValue: false,
            format: function (value, format, lng) {
                if (value instanceof Date) return moment(value).format(format);
                return value;
            }
        },
        react: {
            useSuspense: true
        }
    });

i18n.on('languageChanged', function (lng) {
    moment.locale(lng);
});

export default i18n;