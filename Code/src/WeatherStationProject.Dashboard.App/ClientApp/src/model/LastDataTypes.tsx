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