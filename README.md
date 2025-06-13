# 📇 Prova de Conceito - API de Contatos e Empresas (.NET + EF In-Memory + Swagger + XLSX)

Este projeto é uma **prova de conceito** de uma API REST desenvolvida em **C# com ASP.NET Core**, utilizando o padrão de arquitetura **Controller → Service → Repository**, com **injeção de dependência**, **Entity Framework Core com banco em memória**, e interface de usuário via **Swagger**. A API permite o **gerenciamento de contatos e empresas** (CRUD) e **leitura/retorno de arquivos Excel (XLSX)** por meio de endpoints.

Obviamente que essa solução é apenas uma prova de conceito, com mais tempo eu poderia desenvolver um front apropriado e usar um banco de dados real, e não apenas um mockado em memória. Porém, todo o MVP do projeto está desenvolvido aqui. E a aplicação consegue atingir persistência de dados por meio do endpoint de exportar a base em Excel.

---

## 🚀 Funcionalidades

- [x] CRUD completo para **Contatos**
- [x] CRUD completo para **Empresas**
- [x] Interface de testes via **Swagger UI**
- [x] **Leitura** de arquivos `.xlsx` enviados pela API
- [x] **Geração** e retorno de arquivos `.xlsx` com dados registrados

---

## 🧱 Tecnologias e Conceitos Utilizados

- **C# / .NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core (InMemory)**
- **Swagger (Swashbuckle.AspNetCore)**
- **EPPlus** para manipulação de arquivos Excel
- **Padrões de projeto:**
  - Separação em camadas: **Controller**, **Service**, **Repository**
  - **Injeção de dependência** via `IServiceCollection`

---

## ▶️ Como clonar e executar o projeto

Para rodar a API localmente, siga os passos abaixo. Certifique-se de ter o **.NET 8 SDK** instalado na máquina.

### 🔧 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Git (opcional, mas recomendado)

### 1. Clone o repositório
### 2. Use a versão gratuita do Visual Studio para rodar a API localmente
### 3. Acesse o swagger e utilize a aplicação

![image](https://github.com/user-attachments/assets/b698f02b-8513-4e5b-9e15-be4ce5c4a60b)

