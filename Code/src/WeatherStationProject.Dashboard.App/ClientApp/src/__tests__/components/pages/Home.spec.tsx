import { render, screen } from '@testing-library/react';
import React from 'react';
import Home from '../../../components/pages/Home';

jest.mock('react-i18next', () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
    };
  },
}));

describe('Home', () => {
  it('When_RenderingComponent_Should_RenderExpectedContent', () => {
    render(<Home />);

    const element = screen.queryByTestId('home-id');
    expect(element).toBeInTheDocument();
    expect(element?.tagName.toLowerCase()).toEqual('h1');
  });
});
