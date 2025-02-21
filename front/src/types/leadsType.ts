export interface JobInvitation {
    id: string;
    contactFirstName: string;
    contactFullName: string | null;
    contactPhoneNumber: string | null;
    contactEmail: string;
    dateCreated: string;
    suburb: string;
    category: string;
    description: string;
    price: number;
    status: number;
  }
  