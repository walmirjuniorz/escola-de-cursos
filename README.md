# Escola de Cursos

## Projeto

Desenvolvido durante o curso Fullstack da [Academia do Programador](https://www.academiadoprogramador.net) 2026

Uma escola de cursos profissionalizantes oferece diversas formações presenciais e online para alunos que desejam desenvolver novas habilidades e ingressar no mercado de trabalho.

Os alunos da Academia do Programador foram contratados para desenvolver um aplicativo web responsável por gerenciar toda a estrutura acadêmica da escola, permitindo a gestão das informações de cursos, instrutores, turmas e matrículas.

## Funcionalidades

### 1. Módulo de Alunos

#### Requisitos Funcionais

- O sistema deve permitir registrar novos alunos.
- O sistema deve permitir visualizar todos os alunos cadastrados.
- O sistema deve permitir editar alunos existentes.
- O sistema deve permitir excluir alunos cadastrados.

#### Regras de Negócio

- Campos obrigatórios:
  - Nome (3-100 caracteres)
  - Email (válido)
  - CPF (11 dígitos)
- O sistema não deve permitir o cadastro de alunos com o mesmo CPF.

### 2. Módulo de Instrutores

#### Requisitos Funcionais

- O sistema deve permitir registrar novos instrutores.
- O sistema deve permitir visualizar todos os instrutores cadastrados.
- O sistema deve permitir editar instrutores existentes.
- O sistema deve permitir excluir instrutores cadastrados.

#### Regras de Negócio

- Campos obrigatórios:
  - Nome (3-100 caracteres)
  - Telefone (formatos válidos: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
  - CPF (11 dígitos)
- O sistema não deve permitir o cadastro de instrutores com o mesmo telefone ou CPF.

### 3. Módulo de Cursos e Aulas

#### Requisitos Funcionais

- O sistema deve permitir registrar novos cursos
- O sistema deve permitir visualizar todos os cursos cadastrados
- O sistema deve permitir editar cursos existentes
- O sistema deve permitir excluir cursos cadastrados
- O sistema deve permitir adicionar aulas à cursos
- O sistema deve permitir visualizar aulas de cursos
- O sistema deve permitir editar aulas de cursos
- O sistema deve permitir excluir aulas de cursos

#### Regras de Negócio

#### Curso
- Campos obrigatórios:
  - Nome (2-100 caracteres)
  - Nível do Curso (Iniciante, Intermediário, Avançado)
  - Carga Horária (valor positivo, 2-100 horas)
  - Aulas
- O sistema não deve permitir cadastro de cursos com mesmo Nome

#### Aula
- Campos obrigatórios:
  - Nome (2-100 caracteres)
  - Duração (Minutos)
  - Ordem (número inteiro, para ordenação)
  - Curso (que pertence)
- O sistema não deve permitir cadastro de aulas com o mesmo Nome ou Ordem dentro do mesmo
curso

#### 4. Módulo de Turmas e Matrículas

Requisitos Funcionais
- O sistema deve permitir registrar novos turmas
- O sistema deve permitir visualizar todos os turmas cadastrados
- O sistema deve permitir editar turmas existentes
- O sistema deve permitir excluir turmas cadastrados
- O sistema deve permitir adicionar matrículas em cursos
- O sistema deve permitir visualizar matrículas de cursos
- O sistema deve permitir editar matrículas de cursos
- O sistema deve permitir excluir matrículas de cursos

### Regras de Negócio

#### Turma
- Campos obrigatórios:
  - Nome (2-100 caracteres)
  - Curso (obrigatório)
  - Instrutor (obrigatório)
  - Número Máximo de Alunos (valor positivo maior que 0)
  - Data de Início (obrigatória)
  - Data de Término (obrigatória)
  - Matrículas

#### Matrícula
- Campos obrigatórios:
  - Aluno
  - Turma

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

   ```bash
   dotnet restore
   ```

4. Para executar o projeto compilando em tempo real

   ```bash
   dotnet run --project WebApp/EscolaDeCursos.WebApp.csproj
   ```

## Requisitos

- .NET 10.0 SDK
