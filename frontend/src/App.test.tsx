import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

jest.mock('@azure/msal-browser', () => {
  return {
    PublicClientApplication: jest.fn().mockImplementation((config: any) => {})
  };
});


test('renders learn react link', () => {
  render(<App />);
  const linkElement = screen.getByText(/PartyKLINer/i);
  expect(linkElement).toBeInTheDocument();
});
