import styled from 'styled-components';

export const PageContainer = styled.main`
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 24px;
`;

export const LoginCard = styled.div`
  width: 100%;
  max-width: 420px;
`;

export const Title = styled.h1`
  margin: 0 0 8px;
`;

export const Subtitle = styled.p`
  margin: 0 0 32px;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: 16px;
`;

export const Field = styled.div`
  display: flex;
  flex-direction: column;
  gap: 6px;
`;

export const Label = styled.label`
  font-weight: 600;
`;

export const Input = styled.input`
  width: 100%;
  padding: 12px;
  box-sizing: border-box;
`;

export const Button = styled.button`
  width: 100%;
  padding: 12px;
  cursor: pointer;
`;

export const ErrorMessage = styled.p`
  margin: 0;
`;

export const RegisterLink = styled.p`
  margin: 24px 0 0;
  text-align: center;
`;