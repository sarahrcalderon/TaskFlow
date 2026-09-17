import api from '../api/api';
import type {
  CreateProjectRequest,
  Project,
} from '../types/project.types';

export async function getProjects(): Promise<Project[]> {
  const response = await api.get<Project[]>('/Projects');

  return response.data;
}

export async function createProject(
  request: CreateProjectRequest,
): Promise<Project> {
  const response = await api.post<Project>('/Projects', request);

  return response.data;
}