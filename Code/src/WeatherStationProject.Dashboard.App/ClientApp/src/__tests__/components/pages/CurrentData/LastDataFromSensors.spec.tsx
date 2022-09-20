import { render, screen } from '@testing-library/react';
import LastDataFromSensors from '../../../../components/pages/CurrentData/LastDataFromSensors';
import React from 'react';
import { FAKE_LAST_DATA } from '../../../../model/LastDataTypes';

jest.mock('../../../../components/Loading', () => () => <span data-testid="loading-id" />);
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
    jest
      .spyOn(React, 'useState')
      .mockReturnValueOnce([FAKE_LAST_DATA, jest.fn()])
      .mockReturnValueOnce([false, jest.fn()]);

    render(<LastDataFromSensors />);

    const loading = screen.queryByTestId('loading-id');
    expect(loading).toBeNull();
  });
});
