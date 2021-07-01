export interface ILastData
{
    "air-parameters": IAirParameters;
    "ambient-temperatures": IAmbientTemperatures;
    "ground-temperatures": IGroundTemperatures;
    rainfall: IRainfall;
    "wind-measurements": IWindMeasurements;
    "wind-measurements-gust": IWindMeasurementsGust;
}

interface IAirParameters {
    dateTime: Date;
    pressure: Number;
    humidity: Number;
}

interface IAmbientTemperatures {
    dateTime: Date;
    temperature: Number;
}

interface IGroundTemperatures {
    dateTime: Date;
    temperature: Number;
}

interface IRainfall {
    fromDateTime: Date;
    toDateTime: Date;
    amount: Number;
}

interface IWindMeasurements {
    dateTime: Date;
    speed: Number;
    direction: String;
}

interface IWindMeasurementsGust {
    dateTime: Date;
    speed: Number;
    direction: String;
}