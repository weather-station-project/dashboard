import { render, screen } from '@testing-library/react';
import React from 'react';
import SearchForm from '../../../components/search-form/search-form';
import userEvent from '@testing-library/user-event';
import { ChartValues, GroupingValues } from '../../../model/HistoricalDataTypes';

jest.mock('react-i18next', () => ({
  useTranslation: () => {
    return {
      t: (str: string) => str,
    };
  },
}));

describe('SearchForm', () => {
  it('When_RenderingComponent_Given_ChartViewAndGrouping_Should_RenderExpectedContent', () => {
    // Arrange
    const elementsAndTypes = {
      'initial-date-id': 'label',
      'final-date-id': 'label',
      'chart-view-id': 'label',
      'grouping-id': 'label',
      'submit-id': 'button',
    };

    // Act
    render(<SearchForm showChartViewAndGrouping={true} onSubmit={jest.fn()} />);

    // Assert
    Object.entries(elementsAndTypes).map(([k, v]) => {
      const element = screen.getByTestId(k);
      expect(element).toBeInTheDocument();
      expect(element.tagName.toLowerCase()).toEqual(v);
    });
  });

  it('When_RenderingComponent_Given_NoChartViewNeitherGrouping_Should_RenderExpectedContent', () => {
    // Arrange
    const elementsAndTypesPresent = {
      'initial-date-id': 'label',
      'final-date-id': 'label',
      'submit-id': 'button',
    };
    const elementsNotPresent = ['chart-view-id', 'grouping-id'];

    // Act
    render(<SearchForm showChartViewAndGrouping={false} onSubmit={jest.fn()} />);

    // Assert
    Object.entries(elementsAndTypesPresent).map(([k, v]) => {
      const element = screen.getByTestId(k);
      expect(element).toBeInTheDocument();
      expect(element.tagName.toLowerCase()).toEqual(v);
    });

    elementsNotPresent.map((x) => {
      const element = screen.queryByTestId(x);
      expect(element).toBeNull();
    });
  });

  it('When_SubmittingForm_Given_AllCorrectValues_Should_SubmitCompleted', () => {
    // Arrange
    const onSubmitting = jest.fn();
    const initialDateValue = '01-01-2022';
    const finalDateValue = '01-01-2022';

    // Act
    render(<SearchForm showChartViewAndGrouping={true} onSubmit={onSubmitting} />);

    userEvent.type(screen.getByTestId('initial-date-input-id') as Element, initialDateValue);
    userEvent.type(screen.getByTestId('final-date-input-id') as Element, finalDateValue);
    userEvent.click(screen.getByTestId('chartView-bars-id') as Element);
    userEvent.selectOptions(screen.getByTestId('select-grouping-id') as Element, [GroupingValues.Months]);
    userEvent.click(screen.getByTestId('submit-id') as Element);

    // Assert
    expect(onSubmitting).toHaveBeenCalled();
    expect(onSubmitting).toHaveBeenCalledWith({
      initialDate: initialDateValue,
      finalDate: finalDateValue,
      grouping: GroupingValues.Months,
      chartView: ChartValues.Bars,
    });
  });
});
