import i18n from "i18next";
import {initReactI18next} from "react-i18next";
import Backend from "i18next-http-backend";
import LanguageDetector from "i18next-browser-languagedetector";

const moment = require("moment/min/moment-with-locales");

i18n
    .use(Backend)
    .use(LanguageDetector)
    .use(initReactI18next)
    .init({
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
    console.debug(`Language changed to '${lng}'`);
});
