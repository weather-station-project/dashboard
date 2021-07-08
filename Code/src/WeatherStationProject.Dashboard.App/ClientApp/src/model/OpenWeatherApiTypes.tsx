export interface IOpenWeatherApiResponse {
    current: ICurrent;
}

interface ICurrent {
    dt: number;
    temp: number;
    pressure: number;
    humidity: number;
    wind_speed: number;
    wind_gust: number;
    wind_deg: number;
    rain: IRainLastHour;
    weather: IWeather[];
}

interface IRainLastHour {
    "1h": number;
}

interface IWeather {
    id: number;
    icon: string;
}

interface IDaily {
    dt: number;
    temp: ITemp;
    pressure: number;
    humidity: number;
    wind_speed: number;
    wind_gust: number;
    wind_deg: number;
    rain: number;
    weather: IWeather[];
}

interface ITemp {
    min: number;
    max: number;
}