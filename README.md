# 📚 Sistema de Biblioteca – Arquitetura de Microsserviços

## 📌 1. Propósito do Sistema

Este projeto implementa o back-end de um sistema de biblioteca usando arquitetura de microsserviços.  
O sistema permite:

- Gerenciar livros; 
- Gerenciar membros; 
- Registrar empréstimos e devoluções;  
- Realizar integrações entre microsserviços:  
  - **2 buscas entre serviços**; 
  - **1 atualização de dados entre serviços**; 

A arquitetura demonstra comunicação entre serviços independentes, cada um com regras, API e banco SQLite próprios.

## ✔️ 1.1 Requisitos Funcionais

### **RF0001 – Cadastro de Livros e Membros**
Cadastrar livros e membros contendo informações essenciais:  
- Livros: título, autor, categoria, quantidade de cópias.  
- Membros: nome, status (ativo/inativo), limite de empréstimos.  

Cada cadastro é salvo no banco local do microsserviço responsável.

### **RF0002 – Registro de Empréstimos e Devoluções**
Registrar empréstimos e devoluções contendo:  
- ID do livro  
- ID do membro  
- Datas  
- Status da operação  


### **RF0003 – Validação Antes do Empréstimo**
Antes de emprestar um livro, o loans-service deve validar via integração:  
- **books-service**: existência do livro e cópias disponíveis  
- **members-service**: existência, status ativo e limite de empréstimos  


### **RF0004 – Atualização Após o Empréstimo**
Após criar um empréstimo:  
- O books-service deve reduzir 1 cópia disponível  
- O members-service (opcional) deve aumentar o total de empréstimos ativos  
Essas atualizações são enviadas pelo loans-service via chamadas **PATCH**.


## 👥 2. Usuários

- **Bibliotecários** – realizam cadastros, consultas e empréstimos.  
- **Membros** – utilizam o serviço para empréstimo de livros.


## 🧩 3. Microsserviços

| Serviço            | Função |
|-------------------|--------|
| **books-service** | Gerencia livros e disponibilidade |
| **members-service** | Gerencia membros e seu status |
| **loans-service** | Registra empréstimos e devoluções |

Cada serviço possui banco e regras independentes.


## 🔗 4. Integrações entre Microsserviços

### 🔍 1. Consulta de Livro  
**loans → books**  
Verifica existência do livro e cópias disponíveis.

### 🔍 2. Consulta de Membro  
**loans → members**  
Valida se o membro existe, está ativo e não excedeu o limite.

### 🔄 3. Atualização Pós-Empréstimo  
**loans → books/members**  
Atualiza quantidade de cópias e empréstimos ativos.

