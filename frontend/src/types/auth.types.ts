export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  userId: string;
  name: string;
  email: string;
  token: string;
}

export interface AuthUser {
  userId: string;
  name: string;
  email: string;
}