import styled from 'styled-components';

export const TaskGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 16px;

  @media (max-width: 700px) {
    grid-template-columns: 1fr;
  }
`;

export const EmptyState = styled.div`
  padding: 40px 24px;
  text-align: center;
  border: 1px dashed rgba(255, 255, 255, 0.15);
  border-radius: 12px;
`;

export const EmptyStateText = styled.p`
  margin: 0;
`;