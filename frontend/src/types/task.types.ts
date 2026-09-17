export type TaskStatus =
  | 'Pending'
  | 'InProgress'
  | 'Completed'
  | 'Cancelled';

export type TaskPriority =
  | 'Low'
  | 'Medium'
  | 'High'
  | 'Critical';

export interface Task {
  id: string;
  title: string;
  description: string;
  status: TaskStatus;
  priority: TaskPriority;
  createdAt: string;
  dueDate: string | null;
  projectId: string;
}

export interface CreateTaskRequest {
  title: string;
  description: string;
  projectId: string;
  priority: TaskPriority;
  dueDate?: string | null;
}