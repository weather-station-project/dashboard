import React from 'react';
import { render, screen } from '@testing-library/react';
import HistoricalData from '../../../../components/pages/HistoricalData/HistoricalData';
import { DefaultHistoricalDataRequest } from '../../../../model/HistoricalDataTypes';

jest.mock('../../../../components/search-form/search-form', () => <span data-testid="search-form-id" />);
jest.mock('../../../../components/pages/HistoricalData/Charts/ChartsList', () => <span data-testid="chart-id" />);
jest.mock('../../../../components/pages/HistoricalData/MeasurementsList', () => <span data-testid="list-id" />);

describe('HistoricalData', () => {
  beforeEach(() => {
    jest
      .spyOn(React, 'useState')
      .mockReturnValueOnce([DefaultHistoricalDataRequest, jest.fn()])
      .mockReturnValueOnce([1, jest.fn()]);
  });

  afterEach(() => {
    const search = screen.getByTestId('search-form-id');
    expect(search).toBeInTheDocument();
    expect(search.tagName.toLowerCase()).toEqual('span');
  });

  it('When_RenderingComponent_Given_ChartAndGrouping_Should_RenderExpectedContent', () => {
    // Act
    render(<HistoricalData showChartViewAndGrouping={true} />);

    // Assert
    const chart = screen.getByTestId('chart-id');
    const list = screen.queryByTestId('list-id');

    expect(chart).toBeInTheDocument();
    expect(chart.tagName.toLowerCase()).toEqual('span');

    expect(list).toBeNull();
  });

  it('When_RenderingComponent_Given_NoChartNorGrouping_Should_RenderExpectedContent', () => {
    // Act
    render(<HistoricalData showChartViewAndGrouping={false} />);

    // Assert
    const chart = screen.queryByTestId('chart-id');
    const list = screen.getByTestId('list-id');

    expect(list).toBeInTheDocument();
    expect(list.tagName.toLowerCase()).toEqual('span');

    expect(chart).toBeNull();
  });
});
