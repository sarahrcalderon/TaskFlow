import styled from 'styled-components';

export const PageContainer = styled.main`
  min-height: 100vh;
  padding: 32px;
`;

export const DashboardContainer = styled.div`
  width: 100%;
  max-width: 1200px;
  margin: 0 auto;
`;

export const Header = styled.header`
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 24px;
  margin-bottom: 40px;
`;

export const HeaderContent = styled.div`
  display: flex;
  flex-direction: column;
  gap: 6px;
`;

export const Brand = styled.span`
  font-size: 14px;
  font-weight: 700;
  letter-spacing: 0.08em;
  text-transform: uppercase;
`;

export const Title = styled.h1`
  margin: 0;
  font-size: 32px;
`;

export const Subtitle = styled.p`
  margin: 0;
`;

export const LogoutButton = styled.button`
  padding: 10px 18px;
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
  margin-bottom: 16px;
`;

export const SectionTitle = styled.h2`
  margin: 0;
  font-size: 20px;
`;

export const StatsGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 16px;

  @media (max-width: 900px) {
    grid-template-columns: repeat(2, 1fr);
  }

  @media (max-width: 520px) {
    grid-template-columns: 1fr;
  }
`;

export const StatCard = styled.div`
  padding: 24px;
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 12px;
`;

export const StatLabel = styled.span`
  display: block;
  margin-bottom: 12px;
  font-size: 14px;
`;

export const StatValue = styled.strong`
  display: block;
  font-size: 32px;
`;

export const ProjectsGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 16px;

  @media (max-width: 900px) {
    grid-template-columns: repeat(2, 1fr);
  }

  @media (max-width: 600px) {
    grid-template-columns: 1fr;
  }
`;

export const ProjectCard = styled.div`
  min-height: 150px;
  padding: 24px;
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 12px;
`;

export const ProjectTitle = styled.h3`
  margin: 0 0 8px;
`;

export const ProjectDescription = styled.p`
  margin: 0;
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

export const QuickActions = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 12px;
`;

export const ActionButton = styled.button`
  padding: 12px 18px;
  cursor: pointer;
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