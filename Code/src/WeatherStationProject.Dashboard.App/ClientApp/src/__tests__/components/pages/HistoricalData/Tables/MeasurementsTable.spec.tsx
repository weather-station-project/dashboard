import { render, screen } from '@testing-library/react';
import React from 'react';
import MeasurementsTable from '../../../../../components/pages/HistoricalData/Tables/MeasurementsTable';

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
jest.mock('react-bootstrap-table2-toolkit', () => {
  return {
    __esModule: true,
    default: () => <span data-testid="toolkit-id" />,
  };
});

describe('MeasurementsTable', () => {
  it('When_RenderingComponent_Given_Options_Should_RenderExpectedContent', () => {
    // Act
    render(<MeasurementsTable measurements={[]} columns={[]} columnNameSort="" csvFilename="" />);

    // Assert
    const toolkit = screen.getByTestId('toolkit-id');

    expect(toolkit).toBeInTheDocument();
    expect(toolkit.tagName.toLowerCase()).toEqual('span');
  });
});
