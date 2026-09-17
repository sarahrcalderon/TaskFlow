import { useEffect, useState, type FormEvent } from 'react';
import {
  createTask,
  updateTask,
  type UpdateTaskRequest,
} from '../../services/taskService';
import type { Task, TaskPriority, TaskStatus } from '../../types/task.types';
import {
  Actions,
  CancelButton,
  ErrorMessage,
  Field,
  Form,
  Header,
  HeaderContent,
  Input,
  Label,
  Modal,
  Overlay,
  Select,
  SubmitButton,
  Subtitle,
  Textarea,
  Title,
} from '../../styles/TaskStyles/TaskModal.styles';

interface TaskModalProps {
  projectId: string;
  task?: Task | null;
  onClose: () => void;
  onCreated: (task: Task) => void;
  onUpdated: (task: Task) => void;
}

export function TaskModal({
  projectId,
  task,
  onClose,
  onCreated,
  onUpdated,
}: TaskModalProps) {
  const isEditing = Boolean(task);

  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [priority, setPriority] = useState<TaskPriority>('Medium');
  const [status, setStatus] = useState<TaskStatus>('Pending');
  const [dueDate, setDueDate] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    setTitle(task?.title ?? '');
    setDescription(task?.description ?? '');
    setPriority(task?.priority ?? 'Medium');
    setStatus(task?.status ?? 'Pending');
    setDueDate(task?.dueDate ? task.dueDate.substring(0, 10) : '');
    setError('');
  }, [task]);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setError('');
    setIsSubmitting(true);

    try {
      if (task) {
        const request: UpdateTaskRequest = {
          title,
          description,
          priority,
          status,
          dueDate: dueDate || null,
        };

        const updatedTask = await updateTask(projectId, task.id, request);

        onUpdated(updatedTask);
      } else {
        const createdTask = await createTask(projectId, {
          title,
          description,
          priority,
          dueDate: dueDate || null,
          projectId,
        });

        onCreated(createdTask);
      }

      onClose();
    } catch {
      setError(
        isEditing
          ? 'Não foi possível atualizar a tarefa.'
          : 'Não foi possível criar a tarefa.',
      );
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <Overlay>
      <Modal>
        <Header>
          <HeaderContent>
            <Title>{isEditing ? 'Editar tarefa' : 'Nova tarefa'}</Title>

            <Subtitle>
              {isEditing
                ? 'Atualize as informações da tarefa.'
                : 'Crie uma tarefa para este projeto.'}
            </Subtitle>
          </HeaderContent>
        </Header>

        <Form onSubmit={handleSubmit}>
          <Field>
            <Label htmlFor="task-title">Título</Label>

            <Input
              id="task-title"
              name="title"
              type="text"
              value={title}
              onChange={(event) => setTitle(event.target.value)}
              placeholder="Ex.: Criar documentação"
              minLength={2}
              maxLength={200}
              required
            />
          </Field>

          <Field>
            <Label htmlFor="task-description">Descrição</Label>

            <Textarea
              id="task-description"
              name="description"
              value={description}
              onChange={(event) => setDescription(event.target.value)}
              placeholder="Descreva o que precisa ser feito"
              minLength={2}
              maxLength={2000}
              required
            />
          </Field>

          <Field>
            <Label htmlFor="task-priority">Prioridade</Label>

            <Select
              id="task-priority"
              name="priority"
              value={priority}
              onChange={(event) =>
                setPriority(event.target.value as TaskPriority)
              }
            >
              <option value="Low">Baixa</option>
              <option value="Medium">Média</option>
              <option value="High">Alta</option>
              <option value="Critical">Crítica</option>
            </Select>
          </Field>

          {isEditing && (
            <Field>
              <Label htmlFor="task-status">Status</Label>

              <Select
                id="task-status"
                name="status"
                value={status}
                onChange={(event) =>
                  setStatus(event.target.value as TaskStatus)
                }
              >
                <option value="Pending">Pendente</option>

                <option value="InProgress">Em andamento</option>

                <option value="Completed">Concluída</option>

                <option value="Cancelled">Cancelada</option>
              </Select>
            </Field>
          )}

          <Field>
            <Label htmlFor="task-due-date">Data de vencimento</Label>

            <Input
              id="task-due-date"
              name="dueDate"
              type="date"
              value={dueDate}
              onChange={(event) => setDueDate(event.target.value)}
            />
          </Field>

          {error && <ErrorMessage>{error}</ErrorMessage>}

          <Actions>
            <CancelButton
              type="button"
              onClick={onClose}
              disabled={isSubmitting}
            >
              Cancelar
            </CancelButton>

            <SubmitButton type="submit" disabled={isSubmitting}>
              {isSubmitting
                ? isEditing
                  ? 'Salvando...'
                  : 'Criando...'
                : isEditing
                  ? 'Salvar alterações'
                  : 'Criar tarefa'}
            </SubmitButton>
          </Actions>
        </Form>
      </Modal>
    </Overlay>
  );
}
