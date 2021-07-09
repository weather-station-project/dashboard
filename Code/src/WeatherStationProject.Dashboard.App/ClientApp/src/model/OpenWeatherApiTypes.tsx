export interface IOpenWeatherApiResponse {
    current: IOpenWeatherApiCurrentDayData;
    daily: IOpenWeatherApiDailyData[];
}

export interface IOpenWeatherApiCurrentDayData {
    dt: number;
    temp: number;
    pressure: number;
    humidity: number;
    wind_speed: number;
    wind_gust: number;
    wind_deg: number;
    rain: IOpenWeatherApiRainLastHourData;
    weather: IOpenWeatherApiWeatherData[];
}

interface IOpenWeatherApiRainLastHourData {
    "1h": number;
}

interface IOpenWeatherApiWeatherData {
    id: number;
    main: string;
    description: string;
    icon: string;
}

export interface IOpenWeatherApiDailyData {
    dt: number;
    temp: IOpenWeatherApiDailyTempData;
    pressure: number;
    humidity: number;
    wind_speed: number;
    wind_gust: number;
    wind_deg: number;
    rain: number;
    weather: IOpenWeatherApiWeatherData[];
}

interface IOpenWeatherApiDailyTempData {
    min: number;
    max: number;
}