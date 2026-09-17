import type { Task } from '../../types/task.types';
import { TaskCard } from '../TaskCard/TaskCard';
import {
  EmptyState,
  EmptyStateText,
  TaskGrid,
} from '../../styles/TaskStyles/TaskList.styles';

interface TaskListProps {
  tasks: Task[];
  onEdit: (task: Task) => void;
  onDelete: (task: Task) => void;
}

export function TaskList({ tasks, onEdit, onDelete }: TaskListProps) {
  if (tasks.length === 0) {
    return (
      <EmptyState>
        <EmptyStateText>Este projeto ainda não possui tarefas.</EmptyStateText>
      </EmptyState>
    );
  }

  return (
    <TaskGrid>
      {tasks.map((task) => (
        <TaskCard
          key={task.id}
          task={task}
          onEdit={onEdit}
          onDelete={onDelete}
        />
      ))}
    </TaskGrid>
  );
}
