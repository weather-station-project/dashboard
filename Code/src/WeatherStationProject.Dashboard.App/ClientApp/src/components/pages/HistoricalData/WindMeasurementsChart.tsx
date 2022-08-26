import React from 'react';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  ChartTypeRegistry,
} from 'chart.js';
import { Chart } from 'react-chartjs-2';

interface IWindMeasurementsChartProps {
  chartType: keyof ChartTypeRegistry;

  speedChartTitle: string;
  directionChartTitle: string;

  avgSpeedTitle: string;
  gustTitle: string;

  keys: string[];
  avgSpeedValues: number[];
  gustValues: number[];
  directionValues: string[];
}

ChartJS.register(CategoryScale, LinearScale, BarElement, PointElement, LineElement, Title, Tooltip, Legend);

const WindMeasurementsChart: React.FC<IWindMeasurementsChartProps> = ({
  chartType,

  speedChartTitle,
  directionChartTitle,

  avgSpeedTitle,
  gustTitle,

  keys,
  avgSpeedValues,
  gustValues,
  directionValues,
}) => {
  const blueColor = 'rgb(0, 128, 255)';
  const blueColorAlpha = 'rgba(0, 128, 255, 0.5)';
  const yellowColor = 'rgb(255, 178, 102)';
  const yellowColorAlpha = 'rgba(255, 178, 102, 0.5)';

  const getOptions = (title: string) => {
    return {
      responsive: true,
      plugins: {
        legend: {
          position: 'top' as const,
        },
        title: {
          display: true,
          text: title,
        },
      },
    };
  };
  const data = {
    labels: keys,
    datasets: [
      {
        type: chartType,
        label: gustTitle,
        data: gustValues,
        backgroundColor: blueColorAlpha,
        borderColor: blueColor,
      },
      {
        type: 'line' as const,
        label: avgSpeedTitle,
        data: avgSpeedValues,
        backgroundColor: yellowColorAlpha,
        borderColor: yellowColor,
        fill: false,
      },
    ],
  };

  return <Chart type={chartType} options={getOptions(speedChartTitle)} data={data} />;
};

export default WindMeasurementsChart;
