import { useEffect, useState, type FormEvent } from 'react';
import { createProject, updateProject } from '../../services/projectService';
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
  project?: Project | null;
  onClose: () => void;
  onCreated: (project: Project) => void;
  onUpdated: (project: Project) => void;
}

export function ProjectModal({
  project,
  onClose,
  onCreated,
  onUpdated,
}: ProjectModalProps) {
  const isEditing = Boolean(project);

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    setName(project?.name ?? '');
    setDescription(project?.description ?? '');
    setError('');
  }, [project]);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    setError('');
    setIsSubmitting(true);

    try {
      if (project) {
        const updatedProject = await updateProject(project.id, {
          name,
          description,
        });

        onUpdated(updatedProject);
      } else {
        const createdProject = await createProject({
          name,
          description,
        });

        onCreated(createdProject);
      }

      onClose();
    } catch {
      setError(
        isEditing
          ? 'Não foi possível atualizar o projeto.'
          : 'Não foi possível criar o projeto.',
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
            <Title>{isEditing ? 'Editar projeto' : 'Novo projeto'}</Title>

            <Subtitle>
              {isEditing
                ? 'Atualize as informações do projeto.'
                : 'Crie um projeto para organizar suas tarefas.'}
            </Subtitle>
          </HeaderContent>

          <CloseButton
            type="button"
            onClick={onClose}
            aria-label="Fechar"
            disabled={isSubmitting}
          >
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
              minLength={2}
              maxLength={2000}
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
              {isSubmitting
                ? isEditing
                  ? 'Salvando...'
                  : 'Criando...'
                : isEditing
                  ? 'Salvar alterações'
                  : 'Criar projeto'}
            </SubmitButton>
          </Actions>
        </Form>
      </Modal>
    </Overlay>
  );
}
