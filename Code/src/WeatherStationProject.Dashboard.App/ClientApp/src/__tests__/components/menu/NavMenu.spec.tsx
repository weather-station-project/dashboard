import { render, screen } from '@testing-library/react';
import NavMenu from '../../../components/menu/NavMenu';
import React from 'react';

jest.mock('../../../components/menu/LanguageSelector', () => () => <span data-testid="lng-id" />);
jest.mock('react-i18next', () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
    };
  },
}));

describe('NavMenu', () => {
  beforeEach(() => {
    render(<NavMenu />);
  });

  it('When_RenderingComponent_Should_RenderExpectedContent', () => {
    const selector = screen.getByTestId('lng-id');
    const image = screen.getByAltText('Logo');

    expect(selector).toBeInTheDocument();
    expect(selector.tagName.toLowerCase()).toEqual('span');

    expect(image).toBeInTheDocument();
    expect(image.tagName.toLowerCase()).toEqual('img');
  });
});
