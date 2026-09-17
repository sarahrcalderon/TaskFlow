import styled from 'styled-components';

export const Card = styled.article`
  display: flex;
  flex-direction: column;
  padding: 20px;
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 12px;
  background: rgba(255, 255, 255, 0.02);
  transition:
    transform 0.2s ease,
    border-color 0.2s ease;

  &:hover {
    transform: translateY(-2px);
    border-color: rgba(255, 255, 255, 0.2);
  }
`;

export const Title = styled.h3`
  margin: 0 0 8px;
  font-size: 18px;
`;

export const Description = styled.p`
  margin: 0 0 18px;
  line-height: 1.5;
`;

export const Meta = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
  margin-top: auto;
`;

export const Badge = styled.span`
  padding: 5px 9px;
  border-radius: 6px;
  background: rgba(255, 255, 255, 0.08);
  font-size: 12px;
  font-weight: 600;
`;

export const Actions = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  margin-top: 18px;
`;

export const ActionButton = styled.button`
  padding: 8px 12px;
  cursor: pointer;
`;

export const DangerButton = styled.button`
  padding: 8px 12px;
  cursor: pointer;
`;