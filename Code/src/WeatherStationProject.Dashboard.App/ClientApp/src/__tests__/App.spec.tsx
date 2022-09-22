import { render, screen } from '@testing-library/react';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import App from '../App';

jest.mock('../components/menu/Layout', () => ({ children }: never) => <>{children}</>);
jest.mock('../components/pages/Home', () => <span data-testid="home-id" />);
jest.mock('../components/pages/CurrentData/CurrentData', () => <></>);
jest.mock('../components/pages/HistoricalData/HistoricalData', () => <></>);
jest.mock('../components/pages/HistoricalData/MeasurementsList', () => <></>);

describe('App', () => {
  beforeEach(() => {
    render(
      <BrowserRouter>
        <App />
      </BrowserRouter>
    );
  });

  it('When_RenderingComponent_Should_RenderExpectedContent', () => {
    const element = screen.getByTestId('home-id');
    expect(element).toBeInTheDocument();
    expect(element.tagName.toLowerCase()).toEqual('span');
  });
});
