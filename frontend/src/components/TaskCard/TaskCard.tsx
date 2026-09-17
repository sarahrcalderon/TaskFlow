import type { Task, TaskPriority, TaskStatus } from '../../types/task.types';
import {
  Card,
  Description,
  Meta,
  Badge,
  Title,
} from '../../styles/TaskStyles/TaskCard.styles';

interface TaskCardProps {
  task: Task;
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

export function TaskCard({ task }: TaskCardProps) {
  return (
    <Card>
      <Title>{task.title}</Title>

      <Description>{task.description}</Description>

      <Meta>
        <Badge>{getStatusLabel(task.status)}</Badge>

        <Badge>{getPriorityLabel(task.priority)}</Badge>

        {task.dueDate && (
          <Badge>
            Vencimento: {new Date(task.dueDate).toLocaleDateString('pt-BR')}
          </Badge>
        )}
      </Meta>
    </Card>
  );
}
