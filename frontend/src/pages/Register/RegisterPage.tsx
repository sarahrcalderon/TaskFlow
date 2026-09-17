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
  LoginLink,
  PageContainer,
  RegisterCard,
  Subtitle,
  Title,
} from '../../styles/RegisterStyles/RegisterPage.styles';

export function RegisterPage() {
  const { register } = useAuth();
  const navigate = useNavigate();

  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setError('');
    setIsSubmitting(true);

    try {
      await register({
        name,
        email,
        password,
      });

      navigate('/dashboard');
    } catch {
      setError('Não foi possível criar a conta.');
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <PageContainer>
      <RegisterCard>
        <Title>Criar conta</Title>

        <Subtitle>Crie sua conta para começar a usar o TaskFlow.</Subtitle>

        <Form onSubmit={handleSubmit}>
          <Field>
            <Label htmlFor="name">Nome</Label>

            <Input
              id="name"
              name="name"
              type="text"
              value={name}
              onChange={(event) => setName(event.target.value)}
              placeholder="Seu nome"
              autoComplete="name"
              required
            />
          </Field>

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
              autoComplete="new-password"
              minLength={6}
              required
            />
          </Field>

          {error && <ErrorMessage>{error}</ErrorMessage>}

          <Button type="submit" disabled={isSubmitting}>
            {isSubmitting ? 'Criando conta...' : 'Criar conta'}
          </Button>
        </Form>

        <LoginLink>
          Já possui uma conta? <Link to="/login">Entrar</Link>
        </LoginLink>
      </RegisterCard>
    </PageContainer>
  );
}
