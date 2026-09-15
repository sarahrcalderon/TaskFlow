# TaskFlow

TaskFlow é uma aplicação full-stack de gerenciamento de projetos e tarefas, desenvolvida com foco em boas práticas de engenharia de software, arquitetura limpa, separação de responsabilidades, APIs REST, autenticação e testes automatizados.

O projeto foi desenvolvido como aplicação de portfólio, com o objetivo de demonstrar conhecimentos práticos em desenvolvimento de software no backend e frontend.

## Sobre o projeto

O TaskFlow permite que usuários criem e gerenciem projetos e tarefas por meio de uma aplicação web.

O backend utiliza ASP.NET Core e segue uma abordagem baseada em Clean Architecture, mantendo separadas as regras de negócio, lógica da aplicação, infraestrutura e camada de API.

O frontend será desenvolvido utilizando React e TypeScript, consumindo o backend por meio de uma API REST.

## Funcionalidades

### Autenticação

* Cadastro de usuários
* Login
* Hash de senhas utilizando BCrypt
* Autenticação baseada em JWT
* Proteção de endpoints com autorização
* Identificação do usuário autenticado por meio de claims do JWT

### Projetos

* Criação de projetos
* Listagem dos projetos do usuário autenticado
* Consulta de detalhes de um projeto
* Atualização de projetos
* Exclusão de projetos

### Tarefas

* Criação de tarefas dentro de projetos
* Listagem de tarefas
* Atualização de tarefas
* Alteração do status da tarefa
* Definição de prioridade
* Definição de prazo
* Exclusão de tarefas

### Funcionalidades planejadas

* Perfil do usuário
* Dashboard de projetos
* Filtros de tarefas
* Ordenação de tarefas
* Pesquisa de tarefas
* Paginação
* Persistência da autenticação no frontend
* Interface responsiva
* Testes automatizados do frontend
* Testes de integração do backend
* Ambiente completo com Docker

## Tecnologias

### Backend

* C#
* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* JWT
* BCrypt
* Swagger / OpenAPI
* xUnit

### Frontend

* React
* TypeScript
* Vite
* React Hooks
* Context API
* REST API
* React Testing Library

### Infraestrutura

* Docker
* Docker Compose
* Git

## Arquitetura

O backend segue os princípios de Clean Architecture, mantendo as regras de negócio independentes de frameworks, banco de dados e detalhes de infraestrutura.

```text
TaskFlow
│
├── backend
│   │
│   ├── TaskFlow.Api
│   │   └── Controllers
│   │
│   ├── TaskFlow.Application
│   │   ├── DTOs
│   │   ├── Interfaces
│   │   ├── Services
│   │   ├── Validators
│   │   └── Mappings
│   │
│   ├── TaskFlow.Domain
│   │   ├── Entities
│   │   ├── Enums
│   │   ├── ValueObjects
│   │   ├── Exceptions
│   │   └── Interfaces
│   │
│   ├── TaskFlow.Infrastructure
│   │   ├── Authentication
│   │   ├── Data
│   │   └── Repositories
│   │
│   └── TaskFlow.Tests
│       ├── Unit
│       └── Integration
│
├── frontend
│
├── docker-compose.yml
│
├── .gitignore
│
└── README.md
```

## Fluxo de dependências

A arquitetura utiliza uma direção de dependências controlada:

```text
TaskFlow.Api
       │
       ▼
TaskFlow.Application
       │
       ▼
TaskFlow.Domain

TaskFlow.Infrastructure
       │
       ▼
TaskFlow.Application
       │
       ▼
TaskFlow.Domain

TaskFlow.Tests
       │
       ├── TaskFlow.Application
       ├── TaskFlow.Domain
       └── TaskFlow.Infrastructure
```

A camada `Domain` não possui dependências relacionadas a Entity Framework, PostgreSQL, ASP.NET Core ou outros detalhes de infraestrutura.

## Camadas do Backend

### TaskFlow.Domain

Contém o núcleo do sistema e as principais regras de negócio.

Responsabilidades:

* Entidades
* Enums
* Value Objects
* Interfaces de domínio
* Exceções de domínio

A camada de domínio permanece independente das tecnologias utilizadas pela aplicação.

### TaskFlow.Application

Contém os casos de uso e a lógica responsável pela orquestração da aplicação.

Responsabilidades:

* DTOs
* Serviços de aplicação
* Interfaces
* Validações
* Mapeamentos
* Contratos de autenticação

### TaskFlow.Infrastructure

Contém as implementações relacionadas a tecnologias externas.

Responsabilidades:

* Entity Framework Core
* PostgreSQL
* DbContext
* Repositórios
* Geração de tokens JWT
* Configuração de injeção de dependência

### TaskFlow.Api

Responsável por disponibilizar a aplicação através de HTTP.

Responsabilidades:

* Controllers
* Configuração de autenticação
* Autorização
* Swagger/OpenAPI
* Endpoints REST
* Configuração da aplicação

### TaskFlow.Tests

Contém os testes automatizados do projeto.

A estrutura permite testar diferentes camadas da aplicação de forma isolada, reduzindo dependências entre testes e facilitando a manutenção do código.

## Banco de dados

O TaskFlow utiliza PostgreSQL como banco de dados principal.

O acesso aos dados é realizado através do Entity Framework Core.

A configuração do ambiente de desenvolvimento utiliza Docker Compose:

```bash
docker compose up -d
```

## Autenticação

A autenticação utiliza JWT.

O fluxo de autenticação é:

```text
Cadastro
   │
   ▼
Usuário criado
   │
   ▼
Senha protegida com BCrypt
   │
   ▼
Login
   │
   ▼
Credenciais validadas
   │
   ▼
JWT gerado
   │
   ▼
Cliente envia Bearer Token
   │
   ▼
API valida o token
   │
   ▼
Endpoint protegido
```

Endpoints de autenticação:

```text
POST /api/Auth/register
POST /api/Auth/login
```

Endpoints protegidos utilizam:

```text
Authorization: Bearer {token}
```

## Swagger

A API possui documentação através do Swagger/OpenAPI.

Após iniciar o backend, a documentação pode ser acessada pelo endereço disponibilizado pelo ASP.NET Core no ambiente de desenvolvimento.

O Swagger permite testar os endpoints diretamente e utilizar autenticação JWT através do botão `Authorize`.


## Testes

O projeto utiliza xUnit para testes automatizados.

Os testes abrangem progressivamente:

* Regras de negócio
* Serviços
* Repositórios
* Autenticação
* Projetos
* Tarefas
* Endpoints da API
* Integração com o banco de dados

O objetivo é manter as principais regras e funcionalidades protegidas por testes automatizados durante a evolução do projeto.

## Objetivos do projeto

O TaskFlow foi desenvolvido com os seguintes objetivos:

* Aplicar princípios de Clean Architecture
* Desenvolver uma API REST utilizando ASP.NET Core
* Trabalhar com Entity Framework Core
* Utilizar PostgreSQL como banco de dados
* Implementar autenticação e autorização com JWT
* Aplicar Repository Pattern
* Utilizar injeção de dependência
* Desenvolver testes automatizados
* Desenvolver uma aplicação frontend com React e TypeScript
* Demonstrar integração entre frontend e backend
* Utilizar Docker para infraestrutura
* Aplicar boas práticas de organização e manutenção de código

## Status

O projeto está em desenvolvimento.

### Backend

* [x] Estrutura inicial
* [x] Clean Architecture
* [x] Entidades principais
* [x] Entity Framework Core
* [x] PostgreSQL
* [x] Migrations
* [x] Repository Pattern
* [x] Cadastro de usuários
* [x] Login
* [x] BCrypt
* [x] JWT
* [x] Autorização
* [x] Swagger
* [ ] Projetos
* [ ] Tarefas
* [ ] Validações completas
* [ ] Testes de integração

### Frontend

* [ ] Estrutura React
* [ ] TypeScript
* [ ] Autenticação
* [ ] Context API
* [ ] Tela de login
* [ ] Dashboard
* [ ] Projetos
* [ ] Tarefas
* [ ] Testes
* [ ] Responsividade


