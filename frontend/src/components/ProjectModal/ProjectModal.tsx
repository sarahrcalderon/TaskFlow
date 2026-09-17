import { useState, type FormEvent } from 'react';
import { createProject } from '../../services/projectService';
import type { Project } from '../../types/project.types';
import {
  Actions,
  CancelButton,
  CloseButton,
  ErrorMessage,
  Field,
  Form,
  Header,
  HeaderContent,
  Input,
  Label,
  Modal,
  Overlay,
  SubmitButton,
  Subtitle,
  Textarea,
  Title,
} from '../../styles/ProjectStyles/ProjectModal.styles';

interface ProjectModalProps {
  onClose: () => void;
  onCreated: (project: Project) => void;
}

export function ProjectModal({ onClose, onCreated }: ProjectModalProps) {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setError('');
    setIsSubmitting(true);

    try {
      const project = await createProject({
        name,
        description,
      });

      onCreated(project);
      onClose();
    } catch {
      setError('Não foi possível criar o projeto.');
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <Overlay>
      <Modal>
        <Header>
          <HeaderContent>
            <Title>Novo projeto</Title>

            <Subtitle>Crie um projeto para organizar suas tarefas.</Subtitle>
          </HeaderContent>

          <CloseButton type="button" onClick={onClose} aria-label="Fechar">
            ×
          </CloseButton>
        </Header>

        <Form onSubmit={handleSubmit}>
          <Field>
            <Label htmlFor="project-name">Nome</Label>

            <Input
              id="project-name"
              name="name"
              type="text"
              value={name}
              onChange={(event) => setName(event.target.value)}
              placeholder="Ex.: Projeto pessoal"
              minLength={2}
              maxLength={100}
              required
            />
          </Field>

          <Field>
            <Label htmlFor="project-description">Descrição</Label>

            <Textarea
              id="project-description"
              name="description"
              value={description}
              onChange={(event) => setDescription(event.target.value)}
              placeholder="Descreva o objetivo do projeto"
              maxLength={500}
              required
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
              {isSubmitting ? 'Criando...' : 'Criar projeto'}
            </SubmitButton>
          </Actions>
        </Form>
      </Modal>
    </Overlay>
  );
}
