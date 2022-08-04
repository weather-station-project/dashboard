import { date, mixed, object, ref, SchemaOf } from 'yup';

export enum GroupingValues {
  Hours = 'Hours',
  Days = 'Days',
  Months = 'Months',
}

export enum ChartValues {
  Bars = 'Bars',
  Lines = 'Lines',
}

export interface IMeasurementsListRequest {
  initialDate: Date | undefined;
  finalDate: Date | undefined;
  grouping: string | undefined;
}

export interface IHistoricalDataRequest extends IMeasurementsListRequest {
  chartView: string | undefined;
}

export const DefaultHistoricalDataRequest: IHistoricalDataRequest = {
  initialDate: undefined,
  finalDate: undefined,
  grouping: undefined,
  chartView: ChartValues.Lines,
};

export const MeasurementsListRequestValidationSchema: SchemaOf<IMeasurementsListRequest> = object({
  initialDate: date().required().max(new Date()),
  finalDate: date().required().min(ref('initialDate')).max(new Date()),
  grouping: mixed<keyof typeof GroupingValues>().required().oneOf(Object.values(GroupingValues)),
});

export const HistoricalDataRequestValidationSchema: SchemaOf<IHistoricalDataRequest> = object({
  initialDate: date().required().max(new Date()),
  finalDate: date().required().min(ref('initialDate')).max(new Date()),
  grouping: mixed<keyof typeof GroupingValues>().required().oneOf(Object.values(GroupingValues)),
  chartView: mixed<keyof typeof ChartValues>().required().oneOf(Object.values(ChartValues)),
});
