# Dashboard para Jovens Estudantes

## Visão Geral

Este dashboard foi implementado para jovens estudantes (tipoUsuario === 0) na plataforma Trabuka, fornecendo uma visão completa de suas atividades, progresso e oportunidades.

## Funcionalidades Implementadas

### 1. Resumo do Dashboard
- **Nível Atual**: Mostra o nível atual do estudante e progresso para o próximo nível
- **Projetos Feitos**: Total de projetos realizados
- **Vagas Aplicadas**: Número de candidaturas enviadas
- **Ganhos Este Mês**: Valor total ganho no mês atual

### 2. Perfil do Usuário
- Foto de perfil
- Informações pessoais
- Habilidades principais
- Certificações
- Currículo

### 3. Vagas Recomendadas
- Lista de vagas baseadas nas habilidades do usuário
- Informações da empresa e localização
- Habilidades requeridas
- Tempo de publicação

### 4. Projetos Recentes
- Lista de projetos em andamento e concluídos
- Status do projeto (Em Andamento, Concluído, Cancelado)
- Valor do projeto
- Datas de início e conclusão

### 5. Pagamentos Recentes
- Histórico de pagamentos recebidos
- Status do pagamento (Pendente, Pago, Cancelado)
- Empresa pagadora
- Data do pagamento

### 6. Testes de Habilidades
- Contador de testes realizados
- Link para testes disponíveis

### 7. Fórum da Comunidade
- Acesso ao fórum para interação com outros estudantes

## Integração com API

### Endpoints Utilizados
- `GET /api/dashboard/resumo/{usuarioId}` - Dados completos do dashboard
- `GET /api/dashboard/estatisticas/{usuarioId}` - Estatísticas rápidas
- `GET /api/dashboard/vagas-recomendadas/{usuarioId}` - Vagas recomendadas
- `GET /api/dashboard/projetos-recentes/{usuarioId}` - Projetos recentes
- `GET /api/dashboard/pagamentos-recentes/{usuarioId}` - Pagamentos recentes

### Fallback para Dados Mock
Quando a API não está disponível, o sistema automaticamente usa dados mock para demonstração, com um indicador visual para o usuário.

## Estrutura de Arquivos

```
dashboard/
├── dashboard.component.ts          # Lógica do componente
├── dashboard.component.html        # Template HTML
├── dashboard.component.css         # Estilos CSS
└── README.md                      # Esta documentação

services/
├── dashboard.service.ts           # Serviço para API real
└── dashboard-mock.service.ts      # Serviço para dados mock
```

## Estados da Interface

### 1. Loading
- Spinner de carregamento
- Mensagem "Carregando dashboard..."

### 2. Error
- Alerta de erro
- Botão para tentar novamente
- Mensagem descritiva do erro

### 3. Success
- Dashboard completo com dados
- Indicador se usando dados mock

### 4. Empty States
- Mensagens quando não há dados
- Ícones ilustrativos

## Responsividade

O dashboard é totalmente responsivo e se adapta a diferentes tamanhos de tela:
- Desktop: Layout em grid 4 colunas
- Tablet: Layout em grid 2 colunas
- Mobile: Layout em coluna única

## Animações

- Fade-in para seções
- Hover effects nos cards
- Transições suaves
- Loading spinner animado

## Cores e Design

### Paleta de Cores
- **Primária**: #b4872d (Dourado)
- **Secundária**: #17495d (Azul escuro)
- **Status**: 
  - Pendente: #ffc107 (Amarelo)
  - Concluído: #28a745 (Verde)
  - Cancelado: #dc3545 (Vermelho)

### Elementos Visuais
- Cards com sombras suaves
- Bordas arredondadas
- Ícones Bootstrap
- Progress bars animados

## Como Usar

1. **Acesso**: O dashboard é acessível através da rota `/dashboard`
2. **Autenticação**: Requer usuário logado com tipoUsuario === 0
3. **Navegação**: Links internos para diferentes seções
4. **Interação**: Botões para ações como "Ver Detalhes", "Editar Perfil"

## Desenvolvimento

### Para adicionar novas funcionalidades:
1. Atualizar a interface `DashboardResumo` em `dashboard.service.ts`
2. Adicionar métodos no `DashboardService`
3. Atualizar o template HTML
4. Adicionar estilos CSS conforme necessário

### Para modificar dados mock:
1. Editar o objeto `mockDashboardData` em `dashboard-mock.service.ts`
2. Manter a estrutura consistente com a interface

## Performance

- Lazy loading de dados
- Cache de requisições
- Otimização de imagens
- Compressão de CSS/JS

## Segurança

- Validação de dados
- Sanitização de inputs
- Controle de acesso por tipo de usuário
- Tratamento seguro de erros 