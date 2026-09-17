export interface Project {
  id: string;
  name: string;
  description: string;
  createdAt: string;
  ownerId: string;
}

export interface CreateProjectRequest {
  name: string;
  description: string;
}