import React, { useState } from 'react';
import { DefaultHistoricalDataRequest, IHistoricalDataRequest } from '../../../model/HistoricalDataTypes';
import { FormikHelpers } from 'formik';
import SearchForm from '../../search-form/search-form';
import ChartsList from './ChartsList';

const HistoricalData: React.FC = () => {
  const [formValues, setFormValues] = useState<IHistoricalDataRequest>(DefaultHistoricalDataRequest);
  const [valuesCompleted, setValuesCompleted] = useState<boolean>(false);

  const handleOnSubmit = (values: IHistoricalDataRequest, helpers: FormikHelpers<IHistoricalDataRequest>) => {
    helpers.setSubmitting(false);
    setFormValues(values);
    setValuesCompleted(true);
  };

  return (
    <>
      <div>
        <SearchForm showChartView={true} onSubmit={handleOnSubmit} />
      </div>
      <div className="mt-5">{valuesCompleted && <ChartsList requestData={formValues} />}</div>
    </>
  );
};

export default HistoricalData;
