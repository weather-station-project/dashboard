import { render, screen } from '@testing-library/react';
import React from 'react';
import ForecastData from '../../../../components/pages/CurrentData/ForecastData';
import { FAKE_CURRENT_CONDITIONS_DATA, FAKE_FORECAST_DATA } from '../../../../model/OpenWeatherApiTypes';

jest.mock('react-i18next', () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
      i18n: {
        language: 'en',
      },
    };
  },
}));

beforeEach(() => {
  jest.spyOn(React, 'useEffect').mockImplementationOnce(() => {
    /**/
  });
});

describe('ForecastData', () => {
  it('When_RenderingComponent_Given_No_Obtained_Data_Should_RenderOverlayButtons', () => {
    render(<ForecastData />);

    const div = screen.queryByTestId('carousel-id');
    expect(div).toBeNull();

    const button = screen.queryByTestId('button-id');
    expect(button).toBeInTheDocument();
    expect(button?.tagName.toLowerCase()).toEqual('button');
  });

  it('When_RenderingComponent_Given_Data_Should_NotRenderOverlayButtons', () => {
    jest
      .spyOn(React, 'useState')
      .mockReturnValueOnce([FAKE_CURRENT_CONDITIONS_DATA, jest.fn()])
      .mockReturnValueOnce([FAKE_FORECAST_DATA, jest.fn()])
      .mockReturnValueOnce([false, jest.fn()]);
    render(<ForecastData />);

    const div = screen.queryByTestId('carousel-id');
    expect(div).toBeInTheDocument();
    expect(div?.tagName.toLowerCase()).toEqual('div');

    const button = screen.queryByTestId('button-id');
    expect(button).toBeNull();
  });
});
