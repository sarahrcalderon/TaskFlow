import { useState, type FormEvent } from 'react';
import { createTask } from '../../services/taskService';
import type { Task, TaskPriority } from '../../types/task.types';
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
  onClose: () => void;
  onCreated: (task: Task) => void;
}

export function TaskModal({ projectId, onClose, onCreated }: TaskModalProps) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [priority, setPriority] = useState<TaskPriority>('Medium');
  const [dueDate, setDueDate] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setError('');
    setIsSubmitting(true);

    try {
      const task = await createTask(projectId, {
        title,
        description,
        priority,
        dueDate: dueDate || null,
        projectId,
      });

      onCreated(task);
      onClose();
    } catch {
      setError('Não foi possível criar a tarefa.');
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <Overlay>
      <Modal>
        <Header>
          <HeaderContent>
            <Title>Nova tarefa</Title>

            <Subtitle>Crie uma tarefa para este projeto.</Subtitle>
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
              maxLength={1000}
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
              {isSubmitting ? 'Criando...' : 'Criar tarefa'}
            </SubmitButton>
          </Actions>
        </Form>
      </Modal>
    </Overlay>
  );
}
