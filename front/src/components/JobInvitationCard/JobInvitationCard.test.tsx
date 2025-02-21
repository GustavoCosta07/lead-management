import { render, screen, fireEvent } from '@testing-library/react';
import JobInvitationCard from './JobInvitationCard';
import { JobInvitation } from '../../types/leadsType';
import { vi } from 'vitest';


const mockInvitation: JobInvitation = {
  id: '1',
  contactFirstName: 'John',
  dateCreated: '2023-01-01T00:00:00Z',
  suburb: 'Suburbia',
  category: 'Plumbing',
  price: 100,
  contactPhoneNumber: '123456789',
  contactEmail: 'john@example.com',
  description: 'Fix leaky faucet',
  contactFullName: null,
  status: 0
};

describe('JobInvitationCard Component', () => {
  it('should render job invitation details', () => {
    render(
      <JobInvitationCard
        invitation={mockInvitation}
        onAccept={() => {}}
        onDecline={() => {}}
        isAcceptedTab={false}
      />
    );

    expect(screen.getByText('John')).toBeInTheDocument();
    expect(screen.getByText('Suburbia')).toBeInTheDocument();
    expect(screen.getByText('Plumbing')).toBeInTheDocument();
    expect(screen.getByText('Fix leaky faucet')).toBeInTheDocument();
  });

  it('should call onAccept when the Accept button is clicked', () => {
    const handleAccept = vi.fn();
    render(
      <JobInvitationCard
        invitation={mockInvitation}
        onAccept={handleAccept}
        onDecline={() => {}}
        isAcceptedTab={false}
      />
    );

    fireEvent.click(screen.getByText('Accept'));
    expect(handleAccept).toHaveBeenCalledWith('1');
  });

  it('should call onDecline when the Decline button is clicked', () => {
    const handleDecline = vi.fn();
    render(
      <JobInvitationCard
        invitation={mockInvitation}
        onAccept={() => {}}
        onDecline={handleDecline}
        isAcceptedTab={false}
      />
    );

    fireEvent.click(screen.getByText('Decline'));
    expect(handleDecline).toHaveBeenCalledWith('1');
  });
});
