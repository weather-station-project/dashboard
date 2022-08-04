import CurrentData from '../../../../components/pages/CurrentData/CurrentData';
import { render, screen } from '@testing-library/react';
import React from 'react';

jest.mock('../../../../components/pages/CurrentData/LastDataFromSensors', () => () => <span data-testid="last-id" />);
jest.mock('../../../../components/pages/CurrentData/ForecastData', () => () => <span data-testid="forecast-id" />);
jest.mock('react-i18next', () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
    };
  },
}));

describe('CurrentData', () => {
  beforeEach(() => {
    render(<CurrentData />);
  });

  it('When_RenderingComponent_Should_RenderExpectedContent', () => {
    const lastData = screen.queryByTestId('last-id');
    expect(lastData).toBeInTheDocument();
    expect(lastData?.tagName.toLowerCase()).toEqual('span');

    const forecastData = screen.queryByTestId('forecast-id');
    expect(forecastData).toBeInTheDocument();
    expect(forecastData?.tagName.toLowerCase()).toEqual('span');

    const h11 = screen.queryByTestId('h1-1-id');
    expect(h11).toBeInTheDocument();
    expect(h11?.tagName.toLowerCase()).toEqual('h1');

    const h12 = screen.queryByTestId('h1-2-id');
    expect(h12).toBeInTheDocument();
    expect(h12?.tagName.toLowerCase()).toEqual('h1');
  });
});
