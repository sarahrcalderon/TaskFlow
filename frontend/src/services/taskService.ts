import api from '../api/api';
import type {
  CreateTaskRequest,
  Task,
  TaskPriority,
  TaskStatus,
} from '../types/task.types';

export interface UpdateTaskRequest {
  title: string;
  description: string;
  priority: TaskPriority;
  status: TaskStatus;
  dueDate?: string | null;
}

export async function getTasks(
  projectId: string,
): Promise<Task[]> {
  const response = await api.get<Task[]>(
    `/Projects/${projectId}/Tasks`,
  );

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

export async function updateTask(
  projectId: string,
  taskId: string,
  request: UpdateTaskRequest,
): Promise<Task> {
  const response = await api.put<Task>(
    `/Projects/${projectId}/Tasks/${taskId}`,
    request,
  );

  return response.data;
}

export async function deleteTask(
  projectId: string,
  taskId: string,
): Promise<void> {
  await api.delete(
    `/Projects/${projectId}/Tasks/${taskId}`,
  );
}