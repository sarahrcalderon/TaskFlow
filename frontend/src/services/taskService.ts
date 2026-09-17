import api from '../api/api';
import type {
  CreateTaskRequest,
  Task,
} from '../types/task.types';

export async function getTasks(projectId: string): Promise<Task[]> {
  const response = await api.get<Task[]>(`/Projects/${projectId}/Tasks`);

  return response.data;
}

export async function createTask(
  projectId: string,
  request: CreateTaskRequest,
): Promise<Task> {
  const response = await api.post<Task>(
    `/Projects/${projectId}/Tasks`,
    request,
  );

  return response.data;
}