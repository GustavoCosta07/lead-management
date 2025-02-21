import { vi } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import Tabs from './Tabs';

describe('Tabs Component', () => {
  it('should render tabs and switch between them', () => {
    const setActiveTab = vi.fn();
    render(
      <Tabs tabs={['Tab 1', 'Tab 2']} setActiveTab={setActiveTab}>
        <div>Content 1</div>
        <div>Content 2</div>
      </Tabs>
    );

    expect(screen.getByText('Tab 1')).toBeInTheDocument();
    expect(screen.getByText('Tab 2')).toBeInTheDocument();
    expect(screen.getByText('Content 1')).toBeInTheDocument();

    fireEvent.click(screen.getByText('Tab 2'));
    expect(setActiveTab).toHaveBeenCalledWith('Tab 2');
    expect(screen.getByText('Content 2')).toBeInTheDocument();
  });
});
