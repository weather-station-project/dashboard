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

export interface IHistoricalDataRequest {
  initialDate: Date | undefined;
  finalDate: Date | undefined;
  grouping: string | undefined;
  chartView: string | undefined;
}

export const DefaultHistoricalDataRequest: IHistoricalDataRequest = {
  initialDate: undefined,
  finalDate: undefined,
  grouping: undefined,
  chartView: ChartValues.Lines,
};

export const HistoricalDataRequestValidationSchema: SchemaOf<IHistoricalDataRequest> = object({
  initialDate: date()
    .required('historical_data.initial_date.required')
    .max(new Date(), 'historical_data.initial_date.max_date'),
  finalDate: date()
    .required('historical_data.final_date.required')
    .min(ref('initialDate'), 'historical_data.final_date.min_date')
    .max(new Date(), 'historical_data.final_date.max_date'),
  grouping: mixed<keyof typeof GroupingValues>()
    .required('historical_data.grouping.required')
    .oneOf(Object.values(GroupingValues), 'historical_data.grouping.values'),
  chartView: mixed<keyof typeof ChartValues>()
    .required('historical_data.chart_view.required')
    .oneOf(Object.values(ChartValues), 'historical_data.chart_view.values'),
});
