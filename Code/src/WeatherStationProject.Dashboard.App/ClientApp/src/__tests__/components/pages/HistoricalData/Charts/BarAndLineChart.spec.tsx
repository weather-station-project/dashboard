import React from 'react';
import { render, screen } from '@testing-library/react';
import BarAndLineChart from '../../../../../components/pages/HistoricalData/Charts/BarAndLineChart';

jest.mock('react-chartjs-2', () => ({
  Chart: () => <span data-testid="chart-id" />,
}));

describe('BarAndLineChart', () => {
  it('When_RenderingComponent_Given_Options_Should_RenderExpectedContent', () => {
    // Act
    render(
      <BarAndLineChart
        chartType={'line'}
        chartTitle=""
        maxTitle=""
        avgTitle=""
        minTitle=""
        maxBgColor=""
        maxBorderColor=""
        avgBgColor=""
        avgBorderColor=""
        minBgColor=""
        minBorderColor=""
        keys={[]}
        maxValues={[]}
        avgValues={[]}
        minValues={[]}
      />
    );

    // Assert
    const chart = screen.getByTestId('chart-id');

    expect(chart).toBeInTheDocument();
    expect(chart.tagName.toLowerCase()).toEqual('span');
  });
});
