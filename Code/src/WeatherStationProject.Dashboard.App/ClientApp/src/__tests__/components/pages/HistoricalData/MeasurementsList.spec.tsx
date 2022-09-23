import React from 'react';
import { render, screen } from '@testing-library/react';
import {
  DefaultHistoricalDataRequest,
  IHistoricalDataRequest,
  IHistoricalDataResult,
} from '../../../../model/HistoricalDataTypes';
import MeasurementsList from '../../../../components/pages/HistoricalData/MeasurementsList';

jest.mock('../../../../components/pages/HistoricalData/Tables/MeasurementsTable', () => () => (
  <span data-testid="table-id" />
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

describe('MeasurementsList', () => {
  it('When_RenderingComponent_Given_Loading_Should_RenderExpectedContent', () => {
    // Arrange
    jest
      .spyOn(React, 'useState')
      .mockReturnValueOnce([{} as IHistoricalDataResult, jest.fn()])
      .mockReturnValueOnce([true, jest.fn()]);

    // Act
    render(<MeasurementsList requestData={{} as IHistoricalDataRequest} reRenderForcedState={0} />);

    // Assert
    const loading = screen.getByTestId('loading-spinner');
    const table = screen.queryByTestId('table-id');

    expect(loading).toBeInTheDocument();
    expect(loading.tagName.toLowerCase()).toEqual('div');

    expect(table).toBeNull();
  });

  it('When_RenderingComponent_Given_LoadedData_Should_RenderExpectedContent', () => {
    // Arrange
    const FakeData: IHistoricalDataResult = {
      airParameters: { measurements: [] },
      ambientTemperatures: { measurements: [] },
      groundTemperatures: { measurements: [] },
      rainfall: { measurements: [] },
      windMeasurements: { measurements: [] },
    };
    jest.spyOn(React, 'useState').mockReturnValueOnce([FakeData, jest.fn()]).mockReturnValueOnce([false, jest.fn()]);

    // Act
    render(<MeasurementsList requestData={DefaultHistoricalDataRequest} reRenderForcedState={0} />);

    // Assert
    const loading = screen.queryByTestId('loading-spinner');
    const tables = screen.getAllByTestId('table-id');

    expect(tables).toHaveLength(5);
    tables.map((item) => {
      expect(item).toBeInTheDocument();
      expect(item.tagName.toLowerCase()).toEqual('span');
    });

    expect(loading).toBeNull();
  });
});
