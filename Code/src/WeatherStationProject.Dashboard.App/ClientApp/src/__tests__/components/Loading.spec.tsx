import { render, screen } from '@testing-library/react';
import Loading from '../../components/Loading';
import React from 'react';

describe('Loading', () => {
  it('When_RenderingComponent_Should_RenderExpectedContent', () => {
    render(<Loading />);

    const element = screen.getByTestId('loading-spinner');
    expect(element).toBeInTheDocument();
    expect(element.tagName.toLowerCase()).toEqual('div');
  });
});
