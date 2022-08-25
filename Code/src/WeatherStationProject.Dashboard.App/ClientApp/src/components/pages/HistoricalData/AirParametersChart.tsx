import React from 'react';
import { IHistoricalAirParameters } from '../../../model/HistoricalDataTypes';
import { useTranslation } from 'react-i18next';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { Line } from 'react-chartjs-2';

interface IAirparametersChartProps {
  chartType: string;
  historicalData: IHistoricalAirParameters;
}

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);

const AirParametersChart: React.FC<IAirparametersChartProps> = ({ chartType, historicalData }) => {
  const { t } = useTranslation();
  const lineOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top' as const,
      },
      title: {
        display: true,
        text: t('historical_data.chart.air_parameters.air_pressure'),
      },
    },
  };
  const labels = historicalData.summaryByGroupingItem?.map((item) => item.key);
  const data = {
    labels,
    datasets: [
      {
        label: 'Max pressure',
        data: historicalData.summaryByGroupingItem?.map((item) => item.maxPressure),
        borderColor: 'rgb(255, 99, 132)',
        backgroundColor: 'rgba(255, 99, 132, 0.5)',
      },
      {
        label: 'Avg pressure',
        data: historicalData.summaryByGroupingItem?.map((item) => item.avgPressure),
        borderColor: 'rgb(255, 99, 132)',
        backgroundColor: 'rgba(255, 99, 132, 0.5)',
      },
      {
        label: 'Min pressure',
        data: historicalData.summaryByGroupingItem?.map((item) => item.minPressure),
        borderColor: 'rgb(255, 99, 132)',
        backgroundColor: 'rgba(255, 99, 132, 0.5)',
      },
    ],
  };

  return (
    <>
      <h2 data-testid="air-parameters-chart-id">{t('historical_data.chart.air_parameters')}</h2>
      <Line options={lineOptions} data={data} />
    </>
  );
};

export default React.memo(AirParametersChart);
