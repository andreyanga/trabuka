# Trabuka API

API para conectar jovens profissionais a empresas em Angola, com foco em estágios progressivos e remunerados.

## 🚀 Sobre o Projeto

A Trabuka é uma plataforma que visa conectar jovens profissionais angolanos a empresas, facilitando o acesso a oportunidades de estágio e emprego. Esta API fornece todos os endpoints necessários para gerenciar usuários, empresas, projetos, pagamentos, tickets de suporte e portfolios.

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - Para construção da API REST
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server** - Banco de dados
- **Swagger/OpenAPI** - Documentação da API
- **Code First** - Abordagem de desenvolvimento do banco de dados

## 📋 Pré-requisitos

- .NET 8 SDK
- SQL Server (Local ou Azure)
- Visual Studio 2022 ou VS Code

## 🔧 Configuração

1. **Clone o repositório**
   ```bash
   git clone [url-do-repositorio]
   cd TrabukaApi
   ```

2. **Configure a string de conexão**
   - Edite o arquivo `appsettings.json`
   - Atualize a string de conexão `DefaultConnection` com suas credenciais do SQL Server

3. **Execute as migrations**
   ```bash
   dotnet ef database update
   ```

4. **Execute o projeto**
   ```bash
   dotnet run
   ```

5. **Acesse a documentação**
   - Swagger UI: `http://localhost:5006`
   - API Base URL: `http://localhost:5006/api`

## 📚 Estrutura do Projeto

```
TrabukaApi/
├── Controllers/          # Controllers da API
├── Data/                # Contexto do Entity Framework
├── Dtos/                # Data Transfer Objects
├── Helpers/             # Middlewares e utilitários
├── Interfaces/          # Interfaces dos repositórios e serviços
├── Models/              # Entidades do banco de dados
├── Services/            # Implementação dos serviços
└── Migrations/          # Migrations do Entity Framework
```

## 🔌 Endpoints Principais

### Teste
- `GET /api/teste` - Teste básico da API
- `GET /api/teste/{nome}` - Teste com parâmetro
- `GET /api/teste/info` - Informações do sistema

### Usuários
- `GET /api/usuarios` - Lista todos os usuários
- `GET /api/usuarios/{id}` - Obtém usuário por ID
- `POST /api/usuarios` - Cria novo usuário
- `PUT /api/usuarios/{id}` - Atualiza usuário
- `DELETE /api/usuarios/{id}` - Remove usuário
- `GET /api/usuarios/tipo/{tipo}` - Usuários por tipo
- `GET /api/usuarios/status/{status}` - Usuários por status

### Empresas
- `GET /api/empresas` - Lista todas as empresas
- `GET /api/empresas/{id}` - Obtém empresa por ID
- `POST /api/empresas` - Cria nova empresa
- `PUT /api/empresas/{id}` - Atualiza empresa
- `DELETE /api/empresas/{id}` - Remove empresa
- `GET /api/empresas/setor/{setor}` - Empresas por setor
- `GET /api/empresas/status/{status}` - Empresas por status
- `GET /api/empresas/localizacao/{provincia}` - Empresas por localização

### Projetos
- `GET /api/projetos` - Lista todos os projetos
- `GET /api/projetos/{id}` - Obtém projeto por ID
- `POST /api/projetos` - Cria novo projeto
- `PUT /api/projetos/{id}` - Atualiza projeto
- `DELETE /api/projetos/{id}` - Remove projeto
- `GET /api/projetos/empresa/{empresaId}` - Projetos por empresa
- `GET /api/projetos/status/{status}` - Projetos por status
- `GET /api/projetos/tipo/{tipo}` - Projetos por tipo
- `GET /api/projetos/localizacao/{provincia}` - Projetos por localização
- `GET /api/projetos/salario/{minSalario}/{maxSalario}` - Projetos por faixa salarial

### Pagamentos
- `GET /api/pagamentos` - Lista todos os pagamentos
- `GET /api/pagamentos/{id}` - Obtém pagamento por ID
- `POST /api/pagamentos` - Cria novo pagamento
- `PUT /api/pagamentos/{id}` - Atualiza pagamento
- `DELETE /api/pagamentos/{id}` - Remove pagamento
- `GET /api/pagamentos/usuario/{usuarioId}` - Pagamentos por usuário
- `GET /api/pagamentos/empresa/{empresaId}` - Pagamentos por empresa
- `GET /api/pagamentos/status/{status}` - Pagamentos por status
- `GET /api/pagamentos/tipo/{tipo}` - Pagamentos por tipo
- `GET /api/pagamentos/periodo/{dataInicio}/{dataFim}` - Pagamentos por período

### Tickets
- `GET /api/tickets` - Lista todos os tickets
- `GET /api/tickets/{id}` - Obtém ticket por ID
- `POST /api/tickets` - Cria novo ticket
- `PUT /api/tickets/{id}` - Atualiza ticket
- `DELETE /api/tickets/{id}` - Remove ticket
- `PATCH /api/tickets/{id}/status` - Atualiza status do ticket
- `GET /api/tickets/usuario/{usuarioId}` - Tickets por usuário
- `GET /api/tickets/status/{status}` - Tickets por status
- `GET /api/tickets/categoria/{categoria}` - Tickets por categoria
- `GET /api/tickets/prioridade/{prioridade}` - Tickets por prioridade

### Portfolios
- `GET /api/portfolios` - Lista todos os portfolios
- `GET /api/portfolios/{id}` - Obtém portfolio por ID
- `POST /api/portfolios` - Cria novo portfolio
- `PUT /api/portfolios/{id}` - Atualiza portfolio
- `DELETE /api/portfolios/{id}` - Remove portfolio
- `PATCH /api/portfolios/{id}/status` - Atualiza status do portfolio
- `GET /api/portfolios/usuario/{usuarioId}` - Portfolios por usuário
- `GET /api/portfolios/categoria/{categoria}` - Portfolios por categoria
- `GET /api/portfolios/status/{status}` - Portfolios por status
- `GET /api/portfolios/tecnologia/{tecnologia}` - Portfolios por tecnologia

## 🗄️ Modelos de Dados

### Entidades Principais
- **Usuario** - Usuários da plataforma (estudantes, profissionais, empresas)
- **Empresa** - Empresas cadastradas na plataforma
- **Projeto** - Projetos/estágios oferecidos pelas empresas
- **Pagamento** - Sistema de pagamentos e remunerações
- **Ticket** - Sistema de suporte e tickets
- **Portfolio** - Portfolios dos usuários

### Entidades de Suporte
- **FAQ** - Perguntas frequentes
- **Notificacao** - Sistema de notificações
- **Relatorio** - Relatórios e análises
- **Teste** - Testes técnicos
- **ResultadoTeste** - Resultados dos testes
- **Mentoria** - Sistema de mentoria
- **Equipe** - Equipes de trabalho
- **UsuarioEquipe** - Relacionamento usuário-equipe

## 🔐 Segurança

- Tratamento global de exceções
- Validação de dados de entrada
- Logging estruturado
- Hash de senhas (BCrypt)

## 📝 Logs

A API utiliza logging estruturado para monitoramento e debugging. Os logs incluem:
- Informações de requisições
- Erros e exceções
- Operações de negócio importantes

## 🧪 Testes

Para testar a API, você pode:

1. **Usar o Swagger UI** - Acesse `http://localhost:5006` para interface interativa
2. **Usar o arquivo HTTP** - Utilize o arquivo `TrabukaApi.http` com o VS Code ou Rider
3. **Usar Postman** - Importe a coleção do Swagger

## 🔄 Resetar Banco de Dados

Para resetar o banco de dados e executar o seed novamente:

### Windows (PowerShell):
```powershell
cd Backend
.\ResetDatabase.ps1
```

### Linux/Mac (Bash):
```bash
cd Backend
chmod +x ResetDatabase.sh
./ResetDatabase.sh
```

### Manualmente:
```bash
cd Backend
dotnet ef database drop --force --context TrabukaDbContext
dotnet ef database update --context TrabukaDbContext
```

O seed será executado automaticamente quando você iniciar a aplicação (`dotnet run`).

## 🔄 Resetar Banco de Dados

Para resetar completamente o banco de dados e executar o seed novamente:

### Windows (PowerShell):
```powershell
cd Backend
.\ResetDatabase.ps1
```

### Linux/Mac (Bash):
```bash
cd Backend
chmod +x ResetDatabase.sh
./ResetDatabase.sh
```

### Manualmente:
```bash
cd Backend
dotnet ef database drop --force --context TrabukaDbContext
dotnet ef database update --context TrabukaDbContext
```

**Importante:** O seed será executado automaticamente quando você iniciar a aplicação (`dotnet run`). O seed cria:
- ✅ 1 Gestor (ativo)
- ✅ 8 Estudantes (ativos, distribuídos pelos 4 níveis)
- ✅ 2 Estudantes pendentes (para teste de aprovação)
- ✅ 3 Empresas
- ✅ 6 Projetos aprovados (ativos)
- ✅ 2 Projetos pendentes (para teste de aprovação)
- ✅ Candidaturas, relatórios, testes, etc.

## 🔐 Credenciais de Teste (Seed)

Ao executar a API com o banco vazio, o `DatabaseSeeder` cria usuários e dados de exemplo
para facilitar os testes. Use as credenciais abaixo:

### Gestor (Trabuka)
- **Email**: `gestor@trabuka.com`
- **Senha**: `Gestor@123`
- **Tipo de usuário**: Gestor

### Estudantes / Jovens
- **Nível Explorador**
  - Email: `jovem1@trabuka.com` — Senha: `Jovem@123`
  - Email: `jovem2@trabuka.com` — Senha: `Jovem@123`
- **Nível Praticante**
  - Email: `jovem3@trabuka.com` — Senha: `Jovem@123`
  - Email: `jovem4@trabuka.com` — Senha: `Jovem@123`
- **Nível Construtor**
  - Email: `jovem5@trabuka.com` — Senha: `Jovem@123`
  - Email: `jovem6@trabuka.com` — Senha: `Jovem@123`
- **Nível Mestre**
  - Email: `jovem7@trabuka.com` — Senha: `Jovem@123`
  - Email: `jovem8@trabuka.com` — Senha: `Jovem@123`

### Suporte (opcional)
- **Email**: `suporte@trabuka.com`
- **Senha**: `Suporte@123`
- **Tipo de usuário**: Suporte

### Empresas (dados de exemplo)
As empresas de seed são criadas apenas como entidades de negócio (não fazem login na API),
mas você pode consultá‑las pelos endpoints de empresas:
- `Tech Angola` — Setor: Tecnologia
- `EducaMais` — Setor: Educação
- `Saude+ Angola` — Setor: Saúde

## 🚀 Deploy

### Desenvolvimento
```bash
dotnet run
```

### Produção
```bash
dotnet publish -c Release
dotnet run --environment Production
```

## 📞 Suporte

Para suporte técnico ou dúvidas sobre a API, entre em contato:
- Email: contato@trabuka.ao
- Equipe: Trabuka Team

## 📄 Licença

Este projeto está sob a licença [inserir licença].

---

**Trabuka API** - Conectando talentos angolanos ao mercado de trabalho! 🇦🇴 