export interface IOpenWeatherApiResponse {
    current: IOpenWeatherApiCurrentData;
    daily: IOpenWeatherApiDailyData[];
}

export interface IOpenWeatherApiCurrentData {
    dt: number;
    temp: number;
    pressure: number;
    humidity: number;
    uvi: number;
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
    uvi: number;
}

interface IOpenWeatherApiDailyTempData {
    min: number;
    max: number;
}