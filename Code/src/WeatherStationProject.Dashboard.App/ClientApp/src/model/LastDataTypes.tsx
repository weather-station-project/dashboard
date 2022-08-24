export interface ILastData {
  airParameters: IAirParameters;
  ambientTemperatures: IAmbientTemperatures;
  groundTemperatures: IGroundTemperatures;
  rainfallDuringTime: IRainfall;
  windMeasurements: IWindMeasurements;
  windMeasurementsGust: IWindMeasurementsGust;
}

export interface IAirParameters {
  dateTime: Date;
  pressure: number;
  humidity: number;
}

export interface IAmbientTemperatures {
  dateTime: Date;
  temperature: number;
}

export interface IGroundTemperatures {
  dateTime: Date;
  temperature: number;
}

export interface IRainfall {
  fromDateTime: Date;
  toDateTime: Date;
  amount: number;
}

export interface IWindMeasurements {
  dateTime: Date;
  speed: number;
  direction: string;
}

interface IWindMeasurementsGust {
  dateTime: Date;
  speed: number;
  direction: string;
}

export const FAKE_LAST_DATA: ILastData = {
  airParameters: { dateTime: new Date(), pressure: 1, humidity: 1 },
  ambientTemperatures: { dateTime: new Date(), temperature: 1 },
  groundTemperatures: { dateTime: new Date(), temperature: 1 },
  rainfallDuringTime: { fromDateTime: new Date(), toDateTime: new Date(), amount: 1 },
  windMeasurements: { dateTime: new Date(), speed: 1, direction: 'N' },
  windMeasurementsGust: { dateTime: new Date(), speed: 1, direction: 'E' },
};
