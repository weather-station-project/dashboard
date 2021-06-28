export interface ILastData
{
    airParameters: IAirParameters;
    ambientTemperatures: IAmbientTemperatures;
    groundTemperatures: IGroundTemperatures;
    rainfall: IRainfall;
    windMeasurements: IWindMeasurements;
    windMeasurementsGust: IWindMeasurementsGust;
}

interface IAirParameters {
    datetime: Date;
    pressure: Number;
    humidity: Number;
}

interface IAmbientTemperatures {
    datetime: Date;
    temperature: Number;
}

interface IGroundTemperatures {
    datetime: Date;
    temperature: Number;
}

interface IRainfall {
    fromDateTime: Date;
    toDateTime: Date;
    amount: Number;
}

interface IWindMeasurements {
    datetime: Date;
    speed: Number;
    direction: String;
}

interface IWindMeasurementsGust {
    datetime: Date;
    speed: Number;
    direction: String;
}