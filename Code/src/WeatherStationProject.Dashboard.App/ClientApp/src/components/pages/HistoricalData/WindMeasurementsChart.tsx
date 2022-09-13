import React from 'react';
import {
  Chart as ChartJS,
  RadialLinearScale,
  CategoryScale,
  LinearScale,
  BarElement,
  PointElement,
  LineElement,
  Title,
  Filler,
  Tooltip,
  Legend,
  ChartTypeRegistry,
} from 'chart.js';
import { Chart, Radar } from 'react-chartjs-2';
import { IPredominantDirection } from '../../../model/HistoricalDataTypes';
import { blueColor, blueColorAlpha, greenColor, greenColorAlpha, yellowColor, yellowColorAlpha } from './Colors';

interface IWindMeasurementsChartProps {
  chartType: keyof ChartTypeRegistry;

  speedChartTitle: string;
  directionChartTitle: string;

  avgSpeedTitle: string;
  gustTitle: string;

  keys: string[];
  avgSpeedValues: number[];
  gustValues: number[];
  directionValues: IPredominantDirection;
}

ChartJS.register(
  RadialLinearScale,
  CategoryScale,
  LinearScale,
  BarElement,
  PointElement,
  Filler,
  LineElement,
  Title,
  Tooltip,
  Legend
);

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
  const cardinalPoints = [
    'N',
    'N-NE',
    'N-E',
    'E-NE',
    'E',
    'E-SE',
    'S-E',
    'S-SE',
    'S',
    'S-SW',
    'S-W',
    'W-SW',
    'W',
    'W-NW',
    'N-W',
    'N-NW',
  ];
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

  const directionsData = {
    labels: cardinalPoints,
    datasets: [
      {
        label: directionChartTitle,
        data: cardinalPoints.map((point) => (directionValues[point] !== undefined ? directionValues[point] : 0)),
        backgroundColor: greenColorAlpha,
        borderColor: greenColor,
        borderWidth: 1,
      },
    ],
  };

  return (
    <>
      <Chart type={chartType} options={getOptions(speedChartTitle)} data={data} />
      <div className="mt-5 mb-5">
        <Radar
          data={directionsData}
          options={{
            scales: {
              r: {
                suggestedMin: 0,
              },
            },
          }}
        />
      </div>
    </>
  );
};

export default WindMeasurementsChart;
