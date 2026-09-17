import { useState, type FormEvent } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
import {
  Button,
  ErrorMessage,
  Field,
  Form,
  Input,
  Label,
  LoginCard,
  PageContainer,
  RegisterLink,
  Subtitle,
  Title,
} from '../../styles/LoginStyles/LoginPage.styles';

export function LoginPage() {
  const { login } = useAuth();
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setError('');
    setIsSubmitting(true);

    try {
      await login({
        email,
        password,
      });

      navigate('/dashboard');
    } catch {
      setError('Email ou senha inválidos.');
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <PageContainer>
      <LoginCard>
        <Title>TaskFlow</Title>

        <Subtitle>Entre na sua conta para continuar.</Subtitle>

        <Form onSubmit={handleSubmit}>
          <Field>
            <Label htmlFor="email">Email</Label>

            <Input
              id="email"
              name="email"
              type="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              placeholder="seu@email.com"
              autoComplete="email"
              required
            />
          </Field>

          <Field>
            <Label htmlFor="password">Senha</Label>

            <Input
              id="password"
              name="password"
              type="password"
              value={password}
              onChange={(event) => setPassword(event.target.value)}
              placeholder="Sua senha"
              autoComplete="current-password"
              required
            />
          </Field>

          {error && <ErrorMessage>{error}</ErrorMessage>}

          <Button type="submit" disabled={isSubmitting}>
            {isSubmitting ? 'Entrando...' : 'Entrar'}
          </Button>
        </Form>

        <RegisterLink>
          Ainda não possui uma conta? <Link to="/register">Cadastre-se</Link>
        </RegisterLink>
      </LoginCard>
    </PageContainer>
  );
}
