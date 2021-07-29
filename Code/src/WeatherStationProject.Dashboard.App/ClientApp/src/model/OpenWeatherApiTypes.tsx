export interface IAccuWeatherLocationSearchResponse {
    Key: string;
}

export interface IAccuWeatherCurrentConditionsResponse {
    EpochTime: number;
    WeatherIcon: number;
    WeatherText: string;
    Temperature: {
        Metric: {
            Value: number;
        }
    };
    RelativeHumidity: number;
    Wind: {
        Direction: {
            Localized: string;
        },
        Speed: {
            Metric: {
                Value: number;
            }
        }
    };
    WindGust: {
        Speed: {
            Metric: {
                Value: number;
            }
        }
    };
    UVIndexText: string;
    Pressure: {
        Metric: {
            Value: number;
        }
    };
    Precip1hr: {
        Metric: {
            Value: number;
        }
    }
    Link: string;
}

export interface IAccuWeatherForecastResponse {

}