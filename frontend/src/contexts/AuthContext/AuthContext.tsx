import {
  createContext,
  useCallback,
  useEffect,
  useMemo,
  useState,
  type ReactNode,
} from 'react';
import api from '../../api/api';
import type {
  AuthResponse,
  AuthUser,
  LoginRequest,
  RegisterRequest,
} from '../../types/auth.types';

interface AuthContextData {
  user: AuthUser | null;
  token: string | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (request: LoginRequest) => Promise<void>;
  register: (request: RegisterRequest) => Promise<void>;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextData | undefined>(
  undefined,
);

interface AuthProviderProps {
  children: ReactNode;
}

const TOKEN_KEY = 'taskflow_token';
const USER_KEY = 'taskflow_user';

export function AuthProvider({ children }: AuthProviderProps) {
  const [token, setToken] = useState<string | null>(() =>
    localStorage.getItem(TOKEN_KEY),
  );

  const [user, setUser] = useState<AuthUser | null>(() => {
    const storedUser = localStorage.getItem(USER_KEY);

    if (!storedUser) {
      return null;
    }

    try {
      return JSON.parse(storedUser) as AuthUser;
    } catch {
      localStorage.removeItem(USER_KEY);
      return null;
    }
  });

  const [isLoading, setIsLoading] = useState(true);

  const saveSession = useCallback((response: AuthResponse) => {
    const authenticatedUser: AuthUser = {
      userId: response.userId,
      name: response.name,
      email: response.email,
    };

    localStorage.setItem(TOKEN_KEY, response.token);
    localStorage.setItem(USER_KEY, JSON.stringify(authenticatedUser));

    setToken(response.token);
    setUser(authenticatedUser);
  }, []);

  const clearSession = useCallback(() => {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);

    setToken(null);
    setUser(null);
  }, []);

  const login = useCallback(
    async (request: LoginRequest) => {
      const response = await api.post<AuthResponse>('/Auth/login', request);

      saveSession(response.data);
    },
    [saveSession],
  );

  const register = useCallback(
    async (request: RegisterRequest) => {
      const response = await api.post<AuthResponse>('/Auth/register', request);

      saveSession(response.data);
    },
    [saveSession],
  );

  const logout = useCallback(() => {
    clearSession();
  }, [clearSession]);

  useEffect(() => {
    setIsLoading(false);
  }, []);

  const value = useMemo<AuthContextData>(
    () => ({
      user,
      token,
      isAuthenticated: Boolean(token),
      isLoading,
      login,
      register,
      logout,
    }),
    [user, token, isLoading, login, register, logout],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
