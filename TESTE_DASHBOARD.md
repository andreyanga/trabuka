# Como Testar o Dashboard

## Opções de Teste

### 1. Teste com API Real (Recomendado)
Se você tem a API backend rodando na porta 5006:

1. Certifique-se de que a API está rodando em `http://localhost:5006`
2. Verifique se os endpoints do dashboard estão implementados:
   - `GET /api/Dashboard/resumo/{usuarioId}`
   - `GET /api/Dashboard/estatisticas/{usuarioId}`
   - `GET /api/Dashboard/vagas-recomendadas/{usuarioId}`
   - `GET /api/Dashboard/projetos-recentes/{usuarioId}`
   - `GET /api/Dashboard/pagamentos-recentes/{usuarioId}`

3. Execute a aplicação Angular:
   ```bash
   cd trabuka
   npm start
   ```

4. Acesse o dashboard em `http://localhost:4200/dashboard`

### 2. Teste com Dados Mock (Fallback Automático)
Se a API não estiver disponível, o sistema automaticamente usará dados mock:

1. Execute a aplicação Angular:
   ```bash
   cd trabuka
   npm start
   ```

2. Acesse o dashboard em `http://localhost:4200/dashboard`
3. Você verá um alerta amarelo indicando que está usando dados de demonstração

### 3. Forçar Uso de Dados Mock
Para forçar o uso de dados mock mesmo com a API disponível:

1. Edite o arquivo `src/environments/environment.ts`:
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'http://localhost:5006/api',
     useMockData: true, // Mude para true
     mockDelay: 1000
   };
   ```

2. Execute a aplicação Angular:
   ```bash
   cd trabuka
   npm start
   ```

## Estrutura de Dados Esperada

### Resposta da API `/api/dashboard/resumo/{usuarioId}`:
```json
{
  "usuarioId": 1,
  "nome": "João Silva",
  "email": "joao@exemplo.com",
  "fotoPerfil": "/src/assets/images/usuarios/joao.jpg",
  "nivelAtual": 1,
  "nivelNome": "Praticante",
  "progressoNivel": 75,
  "proximoNivel": "Mestre",
  "totalProjetos": 2,
  "projetosConcluidos": 1,
  "projetosEmAndamento": 1,
  "vagasAplicadas": 5,
  "ganhosMesAtual": 35000.00,
  "ganhosMesAnterior": 28000.00,
  "testesRealizados": 3,
  "notificacoesNaoLidas": 2,
  "cv": "CV do João",
  "habilidades": "C#, .NET, JavaScript",
  "habilidadesLista": ["C#", ".NET", "JavaScript"],
  "certificacoes": [
    {
      "nome": ".NET",
      "ano": 2023
    }
  ],
  "vagasRecomendadas": [
    {
      "id": 1,
      "titulo": "Sistema de Gestão Escolar",
      "empresa": "Tech Angola",
      "localizacao": "Luanda",
      "descricao": "Projeto Web com orçamento de Kz 50,000",
      "habilidadesRequeridas": ["C#", ".NET", "JavaScript"],
      "dataPublicacao": "2024-01-15T10:00:00",
      "tempoPublicacao": "há 2 dias"
    }
  ],
  "projetosRecentes": [
    {
      "id": 1,
      "descricao": "Sistema de Gestão Escolar",
      "tipo": "Web",
      "status": 1,
      "dataInicio": "2024-01-15T10:00:00",
      "dataConclusao": "2024-07-15T10:00:00",
      "valor": 50000.00
    }
  ],
  "pagamentosRecentes": [
    {
      "id": 1,
      "valor": 1000.00,
      "data": "2024-01-20T10:00:00",
      "status": 2,
      "empresa": "Tech Angola"
    }
  ]
}
```

## Status Codes

- **1**: Em Andamento (Projetos) / Pendente (Pagamentos)
- **2**: Concluído (Projetos) / Pago (Pagamentos)
- **3**: Cancelado (Projetos e Pagamentos)

## Troubleshooting

### Erro 404 na API
Se você receber erro 404, verifique:
1. Se a API está rodando na porta correta
2. Se os endpoints estão implementados
3. Se a URL está correta (sem duplicação de `/api`)

### Erro de CORS
Se você receber erro de CORS:
1. Configure o CORS no backend para permitir `http://localhost:4200`
2. Ou use um proxy no Angular

### Dados não aparecem
1. Verifique se o usuário está logado
2. Verifique se `user.tipoUsuario === 0` (estudante)
3. Verifique o console do navegador para erros
4. Verifique se a resposta da API está no formato correto

## Logs Úteis

No console do navegador, você verá:
- `"Usando dados mock forçados pela configuração"` - Se `useMockData: true`
- `"API não disponível, usando dados mock"` - Se a API não responde
- `"Erro ao carregar dados da API"` - Se a API responde mas com erro
- `"Usuário não autenticado"` - Se não há usuário logado 