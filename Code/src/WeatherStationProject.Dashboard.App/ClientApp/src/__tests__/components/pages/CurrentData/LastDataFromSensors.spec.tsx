import { render, screen } from '@testing-library/react';
import LastDataFromSensors from '../../../../components/pages/CurrentData/LastDataFromSensors';
import React from 'react';
import { ILastData } from '../../../../model/LastDataTypes';

jest.mock('../../../../components/Loading', () => <span data-testid="loading-id" />);
jest.mock('react-i18next', () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
    };
  },
}));

beforeEach(() => {
  jest.spyOn(React, 'useEffect').mockImplementationOnce(() => {
    /**/
  });
});

describe('LastDataFromSensors', () => {
  it('When_RenderingComponent_Given_Wrong_Obtained_Data_Should_RenderExpectedContent', () => {
    render(<LastDataFromSensors />);

    const loading = screen.getByTestId('loading-id');
    expect(loading).toBeInTheDocument();
    expect(loading.tagName.toLowerCase()).toEqual('span');
  });

  it('When_RenderingComponent_Given_Data_Should_RenderExpectedContent', () => {
    const FakeLastData: ILastData = {
      airParameters: { id: 1, dateTime: new Date(), pressure: 1, humidity: 1 },
      ambientTemperatures: { id: 1, dateTime: new Date(), temperature: 1 },
      groundTemperatures: { id: 1, dateTime: new Date(), temperature: 1 },
      rainfallDuringTime: { id: '1', fromDateTime: new Date(), toDateTime: new Date(), amount: 1 },
      windMeasurements: { id: 1, dateTime: new Date(), speed: 1, direction: 'N' },
      windMeasurementsGust: { id: 1, dateTime: new Date(), speed: 1, direction: 'E' },
    };

    jest
      .spyOn(React, 'useState')
      .mockReturnValueOnce([FakeLastData, jest.fn()])
      .mockReturnValueOnce([false, jest.fn()]);

    render(<LastDataFromSensors />);

    const loading = screen.queryByTestId('loading-id');
    expect(loading).toBeNull();
  });
});
