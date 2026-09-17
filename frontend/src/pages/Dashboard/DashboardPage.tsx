import { useEffect, useState } from 'react';
import { ProjectModal } from '../../components/ProjectModal/ProjectModal';
import { useAuth } from '../../hooks/useAuth';
import {
  getProfile,
  type ProfileResponse,
} from '../../services/profileService';
import { deleteProject, getProjects } from '../../services/projectService';
import { getTasks } from '../../services/taskService';
import type { Project } from '../../types/project.types';
import type { Task } from '../../types/task.types';
import {
  ActionButton,
  DashboardContainer,
  EmptyState,
  EmptyStateText,
  ErrorMessage,
  Header,
  HeaderContent,
  LoadingMessage,
  LogoutButton,
  PageContainer,
  ProjectActionButton,
  ProjectActions,
  ProjectCard,
  ProjectDescription,
  ProjectsGrid,
  ProjectTitle,
  QuickActions,
  Section,
  SectionHeader,
  SectionTitle,
  StatCard,
  StatLabel,
  StatsGrid,
  StatValue,
  Subtitle,
  Title,
  Brand,
} from '../../styles/DashboardStyles/DashboardPage.styles';

export function DashboardPage() {
  const { user, logout } = useAuth();

  const [profile, setProfile] = useState<ProfileResponse | null>(null);
  const [projects, setProjects] = useState<Project[]>([]);
  const [tasks, setTasks] = useState<Task[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [hasError, setHasError] = useState(false);
  const [isProjectModalOpen, setIsProjectModalOpen] = useState(false);

  useEffect(() => {
    async function loadDashboard() {
      try {
        const [profileData, projectsData] = await Promise.all([
          getProfile(),
          getProjects(),
        ]);

        const tasksByProject = await Promise.all(
          projectsData.map((project) => getTasks(project.id)),
        );
        const tasksData = tasksByProject.flat();

        setProfile(profileData);
        setProjects(projectsData);
        setTasks(tasksData);
      } catch {
        setHasError(true);
      } finally {
        setIsLoading(false);
      }
    }

    loadDashboard();
  }, []);

  async function handleDeleteProject(id: string) {
    const confirmed = window.confirm('Deseja realmente excluir este projeto?');

    if (!confirmed) {
      return;
    }

    try {
      await deleteProject(id);

      setProjects((currentProjects) =>
        currentProjects.filter((project) => project.id !== id),
      );

      setTasks((currentTasks) =>
        currentTasks.filter((task) => task.projectId !== id),
      );
    } catch {
      window.alert('Não foi possível excluir o projeto.');
    }
  }

  function handleProjectCreated(project: Project) {
    setProjects((currentProjects) => [project, ...currentProjects]);
  }

  if (isLoading) {
    return <LoadingMessage>Carregando painel...</LoadingMessage>;
  }

  if (hasError) {
    return (
      <ErrorMessage>Não foi possível carregar os dados do painel.</ErrorMessage>
    );
  }

  const displayName = profile?.name ?? user?.name ?? 'Usuário';

  const pendingTasks = tasks.filter((task) => task.status === 'Pending').length;

  const inProgressTasks = tasks.filter(
    (task) => task.status === 'InProgress',
  ).length;

  const completedTasks = tasks.filter(
    (task) => task.status === 'Completed',
  ).length;

  return (
    <PageContainer>
      <DashboardContainer>
        <Header>
          <HeaderContent>
            <Brand>TaskFlow</Brand>

            <Title>Olá, {displayName}!</Title>

            <Subtitle>Aqui está um resumo das suas atividades.</Subtitle>
          </HeaderContent>

          <LogoutButton type="button" onClick={logout}>
            Sair
          </LogoutButton>
        </Header>

        <Section>
          <SectionHeader>
            <SectionTitle>Visão geral</SectionTitle>
          </SectionHeader>

          <StatsGrid>
            <StatCard>
              <StatLabel>Projetos</StatLabel>
              <StatValue>{projects.length}</StatValue>
            </StatCard>

            <StatCard>
              <StatLabel>Tarefas pendentes</StatLabel>
              <StatValue>{pendingTasks}</StatValue>
            </StatCard>

            <StatCard>
              <StatLabel>Em andamento</StatLabel>
              <StatValue>{inProgressTasks}</StatValue>
            </StatCard>

            <StatCard>
              <StatLabel>Concluídas</StatLabel>
              <StatValue>{completedTasks}</StatValue>
            </StatCard>
          </StatsGrid>
        </Section>

        <Section>
          <SectionHeader>
            <SectionTitle>Projetos</SectionTitle>

            <ActionButton
              type="button"
              onClick={() => setIsProjectModalOpen(true)}
            >
              Novo projeto
            </ActionButton>
          </SectionHeader>

          {projects.length === 0 ? (
            <EmptyState>
              <EmptyStateText>Você ainda não possui projetos.</EmptyStateText>
            </EmptyState>
          ) : (
            <ProjectsGrid>
              {projects.map((project) => (
                <ProjectCard key={project.id}>
                  <ProjectTitle>{project.name}</ProjectTitle>

                  <ProjectDescription>{project.description}</ProjectDescription>

                  <ProjectActions>
                    <ProjectActionButton
                      type="button"
                      onClick={() => handleDeleteProject(project.id)}
                    >
                      Excluir
                    </ProjectActionButton>
                  </ProjectActions>
                </ProjectCard>
              ))}
            </ProjectsGrid>
          )}
        </Section>

        <Section>
          <SectionHeader>
            <SectionTitle>Ações rápidas</SectionTitle>
          </SectionHeader>

          <QuickActions>
            <ActionButton
              type="button"
              onClick={() => setIsProjectModalOpen(true)}
            >
              Novo projeto
            </ActionButton>

            <ActionButton type="button">Nova tarefa</ActionButton>
          </QuickActions>
        </Section>
      </DashboardContainer>

      {isProjectModalOpen && (
        <ProjectModal
          onClose={() => setIsProjectModalOpen(false)}
          onCreated={handleProjectCreated}
        />
      )}
    </PageContainer>
  );
}
