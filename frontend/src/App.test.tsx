import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders learn react link', () => {
  jest.mock('./Authentication/MsalService',() => ({ RetrieveToken: jest.fn(() => undefined) }));
  render(<App />);
  const linkElement = screen.getByText(/PartyKLINer/i);
  expect(linkElement).toBeInTheDocument();
});
