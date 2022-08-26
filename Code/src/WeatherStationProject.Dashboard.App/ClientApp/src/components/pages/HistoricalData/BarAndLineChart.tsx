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

interface IBarAndLineChartProps {
  chartType: keyof ChartTypeRegistry;
  chartTitle: string;

  maxTitle: string;
  avgTitle: string;
  minTitle: string;

  maxBgColor: string;
  maxBorderColor: string;
  avgBgColor: string;
  avgBorderColor: string;
  minBgColor: string;
  minBorderColor: string;

  keys: string[];
  maxValues: number[];
  avgValues: number[];
  minValues: number[];
}

ChartJS.register(CategoryScale, LinearScale, BarElement, PointElement, LineElement, Title, Tooltip, Legend);

const BarAndLineChart: React.FC<IBarAndLineChartProps> = ({
  chartType,
  chartTitle,

  maxTitle,
  avgTitle,
  minTitle,

  maxBgColor,
  maxBorderColor,
  avgBgColor,
  avgBorderColor,
  minBgColor,
  minBorderColor,

  keys,
  maxValues,
  avgValues,
  minValues,
}) => {
  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top' as const,
      },
      title: {
        display: true,
        text: chartTitle,
      },
    },
  };
  const data = {
    labels: keys,
    datasets: [
      {
        type: chartType,
        label: maxTitle,
        data: maxValues,
        backgroundColor: maxBgColor,
        borderColor: maxBorderColor,
      },
      {
        type: 'line' as const,
        label: avgTitle,
        data: avgValues,
        backgroundColor: avgBgColor,
        borderColor: avgBorderColor,
        fill: false,
      },
      {
        type: chartType,
        label: minTitle,
        data: minValues,
        backgroundColor: minBgColor,
        borderColor: minBorderColor,
      },
    ],
  };

  return <Chart type={chartType} options={options} data={data} />;
};

export default BarAndLineChart;
