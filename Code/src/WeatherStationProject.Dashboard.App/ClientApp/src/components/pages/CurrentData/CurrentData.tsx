import React from 'react';
import { useTranslation } from 'react-i18next';
import LastDataFromSensors from './LastDataFromSensors';
import ForecastData from './ForecastData';
import { ErrorBoundary } from '../../../ErrorBoundary';

const CurrentData: React.FC = () => {
  const { t } = useTranslation();

  return (
    <div style={{ paddingTop: 20 }}>
      <h1 data-testid="h1-1-id">{t('current_data.last_data_title')}</h1>
      <ErrorBoundary>
        <LastDataFromSensors />
      </ErrorBoundary>
      <br />
      <h1 data-testid="h1-2-id">{t('current_data.forecast_title')}</h1>
      <ErrorBoundary>
        <ForecastData />
      </ErrorBoundary>
    </div>
  );
};

export default CurrentData;
