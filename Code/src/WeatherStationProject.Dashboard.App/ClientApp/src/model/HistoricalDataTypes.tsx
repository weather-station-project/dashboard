import {date, object, ref, SchemaOf, string} from 'yup';

export interface IMeasurementsListRequest {
  initialDate: Date;
  finalDate: Date;
  grouping: string;
}

export interface IHistoricalDataRequest extends IMeasurementsListRequest {
  chartView: string;
}

export const MeasurementsListRequestValidationSchema: SchemaOf<IMeasurementsListRequest> = object({
  initialDate: date().required().max(new Date()),
  finalDate: date().required().min(ref('initialDate')).max(new Date()),
  grouping: string().required().oneOf(['Hours', 'Days', 'Months']),
});

export const HistoricalDataRequestValidationSchema: SchemaOf<IHistoricalDataRequest> = object({
  initialDate: date().required().max(new Date()),
  finalDate: date().required().min(ref('initialDate')).max(new Date()),
  grouping: string().required().oneOf(['Hours', 'Days', 'Months']),
  chartView: string().required().oneOf(['Bars', 'Lines']),
});
