import type { Task } from '../../types/task.types';
import { TaskCard } from '../TaskCard/TaskCard';
import {
  EmptyState,
  EmptyStateText,
  TaskGrid,
} from '../../styles/TaskStyles/TaskList.styles';

interface TaskListProps {
  tasks: Task[];
}

export function TaskList({ tasks }: TaskListProps) {
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
        <TaskCard key={task.id} task={task} />
      ))}
    </TaskGrid>
  );
}
