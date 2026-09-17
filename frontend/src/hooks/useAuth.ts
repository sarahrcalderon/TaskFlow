import { useContext } from 'react';
import { AuthContext } from '../contexts/AuthContext/AuthContext';

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error(
      'useAuth deve ser usado dentro de um AuthProvider. Certifique-se de que o componente que está chamando useAuth esteja envolvido por um AuthProvider.',
    );
  }

  return context;
}