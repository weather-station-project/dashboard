import React from 'react';
import { render, screen } from '@testing-library/react';
import {
  DefaultHistoricalDataRequest,
  IHistoricalDataRequest,
  IHistoricalDataResult,
} from '../../../../../model/HistoricalDataTypes';
import ChartsList from '../../../../../components/pages/HistoricalData/Charts/ChartsList';

jest.mock('../../../../../components/pages/HistoricalData/Charts/BarAndLineChart', () => () => (
  <span data-testid="chart-id" />
));

jest.mock('../../../../../components/pages/HistoricalData/Charts/WindMeasurementsChart', () => () => (
  <span data-testid="chart-id" />
));

jest.mock('react-i18next', () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
    };
  },
}));

jest.spyOn(React, 'useEffect').mockImplementationOnce(() => {
  /**/
});

jest.spyOn(React, 'useRef').mockImplementationOnce(() => {
  return { current: false };
});

describe('ChartsList', () => {
  it('When_RenderingComponent_Given_Loading_Should_RenderExpectedContent', () => {
    // Arrange
    jest
      .spyOn(React, 'useState')
      .mockReturnValueOnce([{} as IHistoricalDataResult, jest.fn()])
      .mockReturnValueOnce([true, jest.fn()]);

    // Act
    render(<ChartsList requestData={{} as IHistoricalDataRequest} reRenderForcedState={0} />);

    // Assert
    const loading = screen.getByTestId('loading-spinner');
    const table = screen.queryByTestId('chart-id');

    expect(loading).toBeInTheDocument();
    expect(loading.tagName.toLowerCase()).toEqual('div');

    expect(table).toBeNull();
  });

  it('When_RenderingComponent_Given_LoadedData_Should_RenderExpectedContent', () => {
    // Arrange
    const FakeData: IHistoricalDataResult = {
      airParameters: {
        summaryByGroupingItem: [
          { key: '', minPressure: 0, minHumidity: 0, maxPressure: 0, maxHumidity: 0, avgPressure: 0, avgHumidity: 0 },
        ],
      },
      ambientTemperatures: {
        summaryByGroupingItem: [{ key: '', maxTemperature: 0, avgTemperature: 0, minTemperature: 0 }],
      },
      groundTemperatures: {
        summaryByGroupingItem: [{ key: '', maxTemperature: 0, avgTemperature: 0, minTemperature: 0 }],
      },
      rainfall: { summaryByGroupingItem: [{ key: '', maxAmount: 0, avgAmount: 0, minAmount: 0 }] },
      windMeasurements: {
        summaryByGroupingItem: [{ key: '', avgSpeed: 0, maxGust: 0 }],
        predominantWindDirections: { NO: 1 },
      },
    };
    jest.spyOn(React, 'useState').mockReturnValueOnce([FakeData, jest.fn()]).mockReturnValueOnce([false, jest.fn()]);

    // Act
    render(<ChartsList requestData={DefaultHistoricalDataRequest} reRenderForcedState={0} />);

    // Assert
    const loading = screen.queryByTestId('loading-spinner');
    const tables = screen.getAllByTestId('chart-id');

    expect(tables).toHaveLength(6);
    tables.map((item) => {
      expect(item).toBeInTheDocument();
      expect(item.tagName.toLowerCase()).toEqual('span');
    });

    expect(loading).toBeNull();
  });
});
