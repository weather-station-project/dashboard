import { render, screen } from '@testing-library/react';
import React from 'react';
import WindMeasurementsChart from '../../../../../components/pages/HistoricalData/Charts/WindMeasurementsChart';

jest.mock('react-chartjs-2', () => ({
  Chart: () => <span data-testid="chart-id" />,
  Radar: () => <span data-testid="radar-id" />,
}));

describe('WindMeasurementsChart', () => {
  it('When_RenderingComponent_Given_Options_Should_RenderExpectedContent', () => {
    // Act
    render(
      <WindMeasurementsChart
        chartType="line"
        speedChartTitle=""
        directionChartTitle=""
        avgSpeedTitle=""
        gustTitle=""
        keys={[]}
        avgSpeedValues={[]}
        gustValues={[]}
        directionValues={{ NO: 1 }}
      />
    );

    // Assert
    const chart = screen.getByTestId('chart-id');
    const radar = screen.getByTestId('radar-id');

    expect(chart).toBeInTheDocument();
    expect(chart.tagName.toLowerCase()).toEqual('span');

    expect(radar).toBeInTheDocument();
    expect(radar.tagName.toLowerCase()).toEqual('span');
  });
});
