import { useEffect, useMemo, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { TaskList } from '../../components/TaskList/TaskList';
import { TaskModal } from '../../components/TaskModal/TaskModal';
import { deleteProject, getProject } from '../../services/projectService';
import { deleteTask, getTasks } from '../../services/taskService';
import type { Project } from '../../types/project.types';
import type { Task } from '../../types/task.types';
import {
  ActionButton,
  BackButton,
  DangerButton,
  Description,
  ErrorMessage,
  HeaderActions,
  HeaderContent,
  LoadingMessage,
  PageContainer,
  ProjectContainer,
  ProjectHeader,
  Section,
  SectionHeader,
  SectionTitle,
  TaskBadge,
  TaskMeta,
  Title,
} from '../../styles/ProjectStyles/ProjectPage.styles';

export function ProjectPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [project, setProject] = useState<Project | null>(null);

  const [tasks, setTasks] = useState<Task[]>([]);

  const [isLoading, setIsLoading] = useState(true);

  const [hasError, setHasError] = useState(false);

  const [isTaskModalOpen, setIsTaskModalOpen] = useState(false);

  const [selectedTask, setSelectedTask] = useState<Task | null>(null);

  useEffect(() => {
    if (!id) {
      setHasError(true);
      setIsLoading(false);
      return;
    }

    const projectId = id;

    async function loadProject() {
      try {
        const [projectData, tasksData] = await Promise.all([
          getProject(projectId),
          getTasks(projectId),
        ]);

        setProject(projectData);
        setTasks(tasksData);
      } catch {
        setHasError(true);
      } finally {
        setIsLoading(false);
      }
    }

    loadProject();
  }, [id]);

  const taskSummary = useMemo(
    () => ({
      pending: tasks.filter((task) => task.status === 'Pending').length,

      inProgress: tasks.filter((task) => task.status === 'InProgress').length,

      completed: tasks.filter((task) => task.status === 'Completed').length,

      cancelled: tasks.filter((task) => task.status === 'Cancelled').length,
    }),
    [tasks],
  );

  async function handleDeleteProject() {
    if (!project) {
      return;
    }

    const confirmed = window.confirm(
      `Deseja realmente excluir o projeto "${project.name}"?`,
    );

    if (!confirmed) {
      return;
    }

    try {
      await deleteProject(project.id);
      navigate('/dashboard');
    } catch {
      window.alert('Não foi possível excluir o projeto.');
    }
  }

  function handleOpenCreateTask() {
    setSelectedTask(null);
    setIsTaskModalOpen(true);
  }

  function handleOpenEditTask(task: Task) {
    setSelectedTask(task);
    setIsTaskModalOpen(true);
  }

  function handleTaskCreated(task: Task) {
    setTasks((currentTasks) => [task, ...currentTasks]);
  }

  function handleTaskUpdated(task: Task) {
    setTasks((currentTasks) =>
      currentTasks.map((currentTask) =>
        currentTask.id === task.id ? task : currentTask,
      ),
    );
  }

  async function handleDeleteTask(task: Task) {
    if (!project) {
      return;
    }

    const confirmed = window.confirm(
      `Deseja realmente excluir a tarefa "${task.title}"?`,
    );

    if (!confirmed) {
      return;
    }

    try {
      await deleteTask(project.id, task.id);

      setTasks((currentTasks) =>
        currentTasks.filter((currentTask) => currentTask.id !== task.id),
      );
    } catch {
      window.alert('Não foi possível excluir a tarefa.');
    }
  }

  function handleCloseTaskModal() {
    setIsTaskModalOpen(false);
    setSelectedTask(null);
  }

  if (isLoading) {
    return <LoadingMessage>Carregando projeto...</LoadingMessage>;
  }

  if (hasError || !project) {
    return <ErrorMessage>Não foi possível carregar o projeto.</ErrorMessage>;
  }

  return (
    <PageContainer>
      <ProjectContainer>
        <BackButton type="button" onClick={() => navigate('/dashboard')}>
          ← Voltar
        </BackButton>

        <ProjectHeader>
          <HeaderContent>
            <Title>{project.name}</Title>

            <Description>{project.description}</Description>
          </HeaderContent>

          <HeaderActions>
            <ActionButton type="button">Editar</ActionButton>

            <DangerButton type="button" onClick={handleDeleteProject}>
              Excluir
            </DangerButton>
          </HeaderActions>
        </ProjectHeader>

        <Section>
          <SectionHeader>
            <SectionTitle>Tarefas ({tasks.length})</SectionTitle>

            <ActionButton type="button" onClick={handleOpenCreateTask}>
              Nova tarefa
            </ActionButton>
          </SectionHeader>

          <TaskList
            tasks={tasks}
            onEdit={handleOpenEditTask}
            onDelete={handleDeleteTask}
          />
        </Section>

        <Section>
          <SectionHeader>
            <SectionTitle>Resumo das tarefas</SectionTitle>
          </SectionHeader>

          <TaskMeta>
            <TaskBadge>Pendentes: {taskSummary.pending}</TaskBadge>

            <TaskBadge>Em andamento: {taskSummary.inProgress}</TaskBadge>

            <TaskBadge>Concluídas: {taskSummary.completed}</TaskBadge>

            <TaskBadge>Canceladas: {taskSummary.cancelled}</TaskBadge>
          </TaskMeta>
        </Section>
      </ProjectContainer>

      {isTaskModalOpen && (
        <TaskModal
          projectId={project.id}
          task={selectedTask}
          onClose={handleCloseTaskModal}
          onCreated={handleTaskCreated}
          onUpdated={handleTaskUpdated}
        />
      )}
    </PageContainer>
  );
}
