import styled from 'styled-components';

export const Overlay = styled.div`
  position: fixed;
  inset: 0;
  z-index: 1000;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 24px;
  background: rgba(0, 0, 0, 0.65);
`;

export const Modal = styled.div`
  width: 100%;
  max-width: 520px;
  max-height: calc(100vh - 48px);
  overflow-y: auto;
  padding: 32px;
  border-radius: 16px;
  background: #151126;
  border: 1px solid rgba(255, 255, 255, 0.12);
  box-sizing: border-box;
`;

export const Header = styled.div`
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 16px;
  margin-bottom: 24px;
`;

export const HeaderContent = styled.div`
  display: flex;
  flex-direction: column;
  gap: 6px;
`;

export const Title = styled.h2`
  margin: 0;
  font-size: 24px;
`;

export const Subtitle = styled.p`
  margin: 0;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: 18px;
`;

export const Field = styled.div`
  display: flex;
  flex-direction: column;
  gap: 7px;
`;

export const Label = styled.label`
  font-weight: 600;
`;

export const Input = styled.input`
  width: 100%;
  padding: 12px;
  box-sizing: border-box;
`;

export const Textarea = styled.textarea`
  width: 100%;
  min-height: 120px;
  padding: 12px;
  resize: vertical;
  box-sizing: border-box;
`;

export const Select = styled.select`
  width: 100%;
  padding: 12px;
  box-sizing: border-box;
`;

export const ErrorMessage = styled.p`
  margin: 0;
`;

export const Actions = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: 12px;
  margin-top: 6px;
`;

export const CancelButton = styled.button`
  padding: 11px 18px;
  cursor: pointer;
`;

export const SubmitButton = styled.button`
  padding: 11px 18px;
  cursor: pointer;
`;