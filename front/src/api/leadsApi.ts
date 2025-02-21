import { JobInvitation } from '../types/leadsType';
import apiClient from './apiClient';

export const getLeads = async (): Promise<JobInvitation[]> => {
  const response = await apiClient.get<JobInvitation[]>('/leads');
  return response.data;
};

export const getLeadsByStatus = async (status: string): Promise<JobInvitation[]> => {
  const response = await apiClient.get<JobInvitation[]>(`/leads/${status}/status`);
  return response.data;
};

export const acceptLead = async (id: string): Promise<void> => {
  await apiClient.post(`/leads/${id}/accept`);
};

export const declineLead = async (id: string): Promise<void> => {
  await apiClient.post(`/leads/${id}/decline`);
};
