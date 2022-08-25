import React, { useState } from 'react';
import { DefaultHistoricalDataRequest, IHistoricalDataRequest } from '../../../model/HistoricalDataTypes';
import { FormikHelpers } from 'formik';
import SearchForm from '../../search-form/search-form';
import ChartsList from './ChartsList';
import { ErrorBoundary } from '../../../ErrorBoundary';
import moment from 'moment';

interface IHistoricalDataProps {
  showChartList: boolean;
}

const HistoricalData: React.FC<IHistoricalDataProps> = ({ showChartList }) => {
  const [formValues, setFormValues] = useState<IHistoricalDataRequest>(DefaultHistoricalDataRequest);
  const [reRenderForcedState, setReRenderForcedState] = useState<number>(0);

  const handleOnSubmit = (values: IHistoricalDataRequest, helpers: FormikHelpers<IHistoricalDataRequest>) => {
    const initialDate = moment(values.initialDate);
    const finalDate = moment(values.finalDate);

    const requestParams = Object.assign({}, values);
    requestParams.initialDate = new Date(
      Date.UTC(initialDate.year(), initialDate.month(), initialDate.date(), 0, 0, 0)
    );
    requestParams.finalDate = new Date(Date.UTC(finalDate.year(), finalDate.month(), finalDate.date(), 23, 59, 59));

    helpers.setSubmitting(false);
    setFormValues(requestParams);
    setReRenderForcedState((rerenderForcedState) => ++rerenderForcedState);
  };

  return (
    <>
      <div>
        <SearchForm showChartView={showChartList} onSubmit={handleOnSubmit} />
      </div>
      <div className="mt-5">
        {reRenderForcedState > 0 && (
          <ErrorBoundary>
            {showChartList && <ChartsList requestData={formValues} reRenderForcedState={reRenderForcedState} />}
          </ErrorBoundary>
        )}
      </div>
    </>
  );
};

export default HistoricalData;
