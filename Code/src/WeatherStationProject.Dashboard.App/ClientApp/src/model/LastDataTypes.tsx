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

export const FAKE_LAST_DATA: ILastData = {
  airParameters: { id: 1, dateTime: new Date(), pressure: 1, humidity: 1 },
  ambientTemperatures: { id: 1, dateTime: new Date(), temperature: 1 },
  groundTemperatures: { id: 1, dateTime: new Date(), temperature: 1 },
  rainfallDuringTime: { id: '1', fromDateTime: new Date(), toDateTime: new Date(), amount: 1 },
  windMeasurements: { id: 1, dateTime: new Date(), speed: 1, direction: 'N' },
  windMeasurementsGust: { id: 1, dateTime: new Date(), speed: 1, direction: 'E' },
};
