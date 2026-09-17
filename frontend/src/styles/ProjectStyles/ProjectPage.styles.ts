import styled from 'styled-components';

export const PageContainer = styled.main`
  min-height: 100vh;
  padding: 32px;
`;

export const ProjectContainer = styled.div`
  width: 100%;
  max-width: 1200px;
  margin: 0 auto;
`;

export const BackButton = styled.button`
  margin-bottom: 24px;
  padding: 8px 14px;
  cursor: pointer;
`;

export const ProjectHeader = styled.header`
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 24px;
  margin-bottom: 40px;

  @media (max-width: 700px) {
    flex-direction: column;
  }
`;

export const HeaderContent = styled.div`
  display: flex;
  flex-direction: column;
  gap: 8px;
`;

export const Title = styled.h1`
  margin: 0;
  font-size: 32px;
`;

export const Description = styled.p`
  margin: 0;
`;

export const HeaderActions = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
`;

export const ActionButton = styled.button`
  padding: 10px 16px;
  cursor: pointer;
`;

export const DangerButton = styled.button`
  padding: 10px 16px;
  cursor: pointer;
`;

export const Section = styled.section`
  margin-bottom: 32px;
`;

export const SectionHeader = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 16px;
  margin-bottom: 18px;

  @media (max-width: 600px) {
    align-items: flex-start;
    flex-direction: column;
  }
`;

export const SectionTitle = styled.h2`
  margin: 0;
  font-size: 22px;
`;

export const TaskGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 16px;

  @media (max-width: 700px) {
    grid-template-columns: 1fr;
  }
`;

export const TaskCard = styled.article`
  padding: 20px;
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 12px;
`;

export const TaskTitle = styled.h3`
  margin: 0 0 8px;
`;

export const TaskDescription = styled.p`
  margin: 0 0 18px;
`;

export const TaskMeta = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
`;

export const TaskBadge = styled.span`
  padding: 5px 9px;
  border-radius: 6px;
  font-size: 12px;
  font-weight: 600;
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

export const LoadingMessage = styled.div`
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
`;

export const ErrorMessage = styled.div`
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
`;