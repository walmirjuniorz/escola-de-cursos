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
