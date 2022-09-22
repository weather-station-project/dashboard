export interface ILastData {
  airParameters: IAirParameters;
  ambientTemperatures: IAmbientTemperatures;
  groundTemperatures: IGroundTemperatures;
  rainfallDuringTime: IRainfall;
  windMeasurements: IWindMeasurements;
  windMeasurementsGust: IWindMeasurementsGust;
}

export interface IAirParameters {
  id: number;
  dateTime: Date;
  pressure: number;
  humidity: number;
}

export interface IAmbientTemperatures {
  id: number;
  dateTime: Date;
  temperature: number;
}

export interface IGroundTemperatures {
  id: number;
  dateTime: Date;
  temperature: number;
}

export interface IRainfall {
  id: string;
  fromDateTime: Date;
  toDateTime: Date;
  amount: number;
}

export interface IWindMeasurements {
  id: number;
  dateTime: Date;
  speed: number;
  direction: string;
}

interface IWindMeasurementsGust {
  id: number;
  dateTime: Date;
  speed: number;
  direction: string;
}
