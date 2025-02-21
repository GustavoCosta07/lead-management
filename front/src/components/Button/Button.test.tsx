import { render, screen, fireEvent } from '@testing-library/react';
import Button from './Button';
import { vi } from 'vitest';


describe('Button Component', () => {
    it('should render the button with the correct text', () => {
        render(<Button onClick={() => { }}>Click Me</Button>);
        expect(screen.getByText('Click Me')).toBeInTheDocument();
    });

    it('should trigger onClick when clicked', () => {
        const handleClick = vi.fn();
        render(<Button onClick={handleClick}>Click Me</Button>);
        fireEvent.click(screen.getByText('Click Me'));
        expect(handleClick).toHaveBeenCalledTimes(1);
    });

    it('should apply custom className and styles', () => {
        render(
            <Button onClick={() => { }} className="custom-class" color="#000" textColor="#fff">
                Custom Button
            </Button>
        );
        const button = screen.getByText('Custom Button');
        expect(button).toHaveClass('custom-class');
        expect(button).toHaveStyle({ backgroundColor: '#000', color: '#fff' });
    });
});
