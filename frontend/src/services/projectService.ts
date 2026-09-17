import api from '../api/api';
import type {
  CreateProjectRequest,
  Project,
} from '../types/project.types';

export async function getProjects(): Promise<Project[]> {
  const response = await api.get<Project[]>('/Projects');

  return response.data;
}

export async function getProject(id: string): Promise<Project> {
  const response = await api.get<Project>(`/Projects/${id}`);

  return response.data;
}

export async function createProject(
  request: CreateProjectRequest,
): Promise<Project> {
  const response = await api.post<Project>('/Projects', request);

  return response.data;
}

export async function updateProject(
  id: string,
  request: CreateProjectRequest,
): Promise<Project> {
  const response = await api.put<Project>(
    `/Projects/${id}`,
    request,
  );

  return response.data;
}

export async function deleteProject(id: string): Promise<void> {
  await api.delete(`/Projects/${id}`);
}