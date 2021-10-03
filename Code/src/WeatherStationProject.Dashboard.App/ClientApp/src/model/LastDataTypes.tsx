export interface ILastData {
    airParameters: IAirParameters;
    ambientTemperatures: IAmbientTemperatures;
    groundTemperatures: IGroundTemperatures;
    rainfall: IRainfall;
    windMeasurements: IWindMeasurements;
    windMeasurementsGust: IWindMeasurementsGust;
}

interface IAirParameters {
    dateTime: Date;
    pressure: number;
    humidity: number;
}

interface IAmbientTemperatures {
    dateTime: Date;
    temperature: number;
}

interface IGroundTemperatures {
    dateTime: Date;
    temperature: number;
}

interface IRainfall {
    fromDateTime: Date;
    toDateTime: Date;
    amount: number;
}

interface IWindMeasurements {
    dateTime: Date;
    speed: number;
    direction: string;
}

interface IWindMeasurementsGust {
    dateTime: Date;
    speed: number;
    direction: string;
}