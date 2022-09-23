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
    };
  };
  RelativeHumidity: number;
  Wind: {
    Direction: {
      Localized: string;
    };
    Speed: {
      Metric: {
        Value: number;
      };
    };
  };
  WindGust: {
    Speed: {
      Metric: {
        Value: number;
      };
    };
  };
  UVIndexText: string;
  Pressure: {
    Metric: {
      Value: number;
    };
  };
  Precip1hr: {
    Metric: {
      Value: number;
    };
  };
  Link: string;
}

export interface IAccuWeatherForecastResponse {
  DailyForecasts: IAccuWeatherDailyForecast[];
}

export interface IAccuWeatherDailyForecast {
  EpochDate: number;
  Temperature: {
    Minimum: {
      Value: number;
    };
    Maximum: {
      Value: number;
    };
  };
  Day: IAccuWeatherForecastDayNightData;
  Night: IAccuWeatherForecastDayNightData;
  Link: string;
}

interface IAccuWeatherForecastDayNightData {
  Icon: number;
  LongPhrase: string;
  Wind: {
    Speed: {
      Value: number;
    };
    Direction: {
      Localized: string;
    };
  };
  WindGust: {
    Speed: {
      Value: number;
    };
    Direction: {
      Localized: string;
    };
  };
  Rain: {
    Value: number;
  };
}

export const FAKE_CURRENT_CONDITIONS_DATA: IAccuWeatherCurrentConditionsResponse = {
  EpochTime: 1,
  WeatherIcon: 0,
  WeatherText: '',
  Temperature: {
    Metric: {
      Value: 0,
    },
  },
  RelativeHumidity: 0,
  Wind: {
    Direction: {
      Localized: 'test',
    },
    Speed: {
      Metric: {
        Value: 0,
      },
    },
  },
  WindGust: {
    Speed: {
      Metric: {
        Value: 0,
      },
    },
  },
  UVIndexText: 'test',
  Pressure: {
    Metric: {
      Value: 0,
    },
  },
  Precip1hr: {
    Metric: {
      Value: 0,
    },
  },
  Link: 'test',
};

export const FAKE_FORECAST_DATA: IAccuWeatherForecastResponse = {
  DailyForecasts: [{} as IAccuWeatherDailyForecast],
};
