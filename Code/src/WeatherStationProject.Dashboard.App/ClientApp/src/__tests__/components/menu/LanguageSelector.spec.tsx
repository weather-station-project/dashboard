import { render, screen } from '@testing-library/react';
import LanguageSelector from '../../../components/menu/LanguageSelector';
import React from 'react';

jest.mock('react-i18next', () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
      i18n: {
        changeLanguage: () =>
          new Promise(() => {
            /**/
          }),
        language: 'en',
      },
    };
  },
}));

describe('LanguageSelector', () => {
  it('When_RenderingComponent_Should_RenderExpectedContent', () => {
    render(<LanguageSelector />);

    const englishOption = screen.queryByTestId('en');
    const spanishOption = screen.queryByTestId('en');

    expect(englishOption).toBeInTheDocument();
    expect(englishOption?.tagName.toLowerCase()).toEqual('option');

    expect(spanishOption).toBeInTheDocument();
    expect(spanishOption?.tagName.toLowerCase()).toEqual('option');
  });
});
