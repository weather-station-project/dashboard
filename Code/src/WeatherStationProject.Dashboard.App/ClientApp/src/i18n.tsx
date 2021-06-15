import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import moment from "moment";
import Backend from "i18next-http-backend";
import LanguageDetector from "i18next-browser-languagedetector";

i18n
    .use(Backend)
    .use(LanguageDetector)
    .use(initReactI18next)
    .init({
        lng: "en",
        fallbackLng: "en",
        debug: false,

        interpolation: {
            escapeValue: false,
            format: (value, format) => {
                if (value instanceof Date) return moment(value).format(format);
                return value;
            }
        },
        react: {
            useSuspense: true
        }
    });

i18n.on("languageChanged", lng => {
    moment.locale(lng);
});

export default i18n;