import type { Task, TaskPriority, TaskStatus } from '../../types/task.types';
import {
  Actions,
  Badge,
  Card,
  Description,
  Meta,
  Title,
  ActionButton,
  DangerButton,
} from '../../styles/TaskStyles/TaskCard.styles';

interface TaskCardProps {
  task: Task;
  onEdit: (task: Task) => void;
  onDelete: (task: Task) => void;
}

function getStatusLabel(status: TaskStatus) {
  const labels: Record<TaskStatus, string> = {
    Pending: 'Pendente',
    InProgress: 'Em andamento',
    Completed: 'Concluída',
    Cancelled: 'Cancelada',
  };

  return labels[status];
}

function getPriorityLabel(priority: TaskPriority) {
  const labels: Record<TaskPriority, string> = {
    Low: 'Baixa',
    Medium: 'Média',
    High: 'Alta',
    Critical: 'Crítica',
  };

  return labels[priority];
}

export function TaskCard({ task, onEdit, onDelete }: TaskCardProps) {
  return (
    <Card>
      <Title>{task.title}</Title>

      <Description>{task.description}</Description>

      <Meta>
        <Badge>Status: {getStatusLabel(task.status)}</Badge>

        <Badge>Prioridade: {getPriorityLabel(task.priority)}</Badge>

        {task.dueDate && (
          <Badge>
            Vencimento: {new Date(task.dueDate).toLocaleDateString('pt-BR')}
          </Badge>
        )}
      </Meta>

      <Actions>
        <ActionButton type="button" onClick={() => onEdit(task)}>
          Editar
        </ActionButton>

        <DangerButton type="button" onClick={() => onDelete(task)}>
          Excluir
        </DangerButton>
      </Actions>
    </Card>
  );
}
