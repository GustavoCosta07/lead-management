import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import JobInvitations from './JobInvitations';
import { getLeads, acceptLead, declineLead } from '../../api/leadsApi';
import { JobInvitation } from '../../types/leadsType';
import { toast } from 'react-toastify';
import { vi } from 'vitest';

vi.mock('../../api/leadsApi');
vi.mock('react-toastify');

const mockLeads: JobInvitation[] = [
  {
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
    status: 0,
  },
  {
    id: '2',
    contactFirstName: 'Jane',
    dateCreated: '2023-01-02T00:00:00Z',
    suburb: 'Downtown',
    category: 'Electrical',
    price: 200,
    contactPhoneNumber: '987654321',
    contactEmail: 'jane@example.com',
    description: 'Fix electrical issue',
    contactFullName: null,
    status: 1,
  },
];

describe('JobInvitations Component', () => {
  beforeEach(() => {
    (getLeads as vi.Mock).mockResolvedValue(mockLeads);
    (acceptLead as vi.Mock).mockResolvedValue({});
    (declineLead as vi.Mock).mockResolvedValue({});
    (toast.success as vi.Mock).mockImplementation(() => {});
    (toast.error as vi.Mock).mockImplementation(() => {});
  });

  it('should render job invitations', async () => {
    render(<JobInvitations />);

    await waitFor(() => {
      expect(screen.getByText('John')).toBeInTheDocument();
    });
  });

  it('should render accepted job invitations', async () => {
    render(<JobInvitations />);

    fireEvent.click(screen.getByText('Accepted'));

    await waitFor(() => {
      expect(screen.getByText('Jane')).toBeInTheDocument();
    });
  });

  it('should handle accept lead', async () => {
    render(<JobInvitations />);

    await waitFor(() => {
      expect(screen.getByText('John')).toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Accept'));

    await waitFor(() => {
      expect(acceptLead).toHaveBeenCalledWith('1');
      expect(toast.success).toHaveBeenCalledWith('Lead accepted, email sent');
    });
  });

  it('should handle decline lead', async () => {
    render(<JobInvitations />);

    await waitFor(() => {
      expect(screen.getByText('John')).toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Decline'));

    await waitFor(() => {
      expect(declineLead).toHaveBeenCalledWith('1');
      expect(toast.success).toHaveBeenCalledWith('Lead declined');
    });
  });

  it('should switch tabs and refresh invitations', async () => {
    render(<JobInvitations />);

    await waitFor(() => {
      expect(screen.getByText('John')).toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Accepted'));

    await waitFor(() => {
      expect(screen.getByText('Jane')).toBeInTheDocument();
    });

    fireEvent.click(screen.getByText('Invited'));

    await waitFor(() => {
      expect(screen.getByText('John')).toBeInTheDocument();
    });
  });
});
