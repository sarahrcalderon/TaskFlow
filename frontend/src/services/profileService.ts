import api from '../api/api';

export interface ProfileResponse {
  userId: string;
  name: string;
  email: string;
}

export async function getProfile(): Promise<ProfileResponse> {
  const response = await api.get<ProfileResponse>('/Profile');

  return response.data;
}