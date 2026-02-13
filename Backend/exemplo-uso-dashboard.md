# Dashboard API - Exemplos de Uso

## Endpoints Disponíveis

### 1. Resumo Completo do Dashboard
```http
GET /api/dashboard/resumo/{usuarioId}
```

**Resposta:**
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
      "tempoPublicacao": "há 2 dias",
      "imagemCapa": "/assets/images/projetos/7c340d49-411c-41aa-bc7f-0113ac0e8958.jpg"
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
      "valor": 50000.00,
      "imagemCapa": "/assets/images/projetos/7c340d49-411c-41aa-bc7f-0113ac0e8958.jpg"
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

### 2. Estatísticas Rápidas
```http
GET /api/dashboard/estatisticas/{usuarioId}
```

**Resposta:**
```json
{
  "nivelAtual": "Praticante",
  "progressoNivel": 75,
  "proximoNivel": "Mestre",
  "totalProjetos": 2,
  "projetosConcluidos": 1,
  "vagasAplicadas": 5,
  "ganhosMesAtual": 35000.00,
  "testesRealizados": 3,
  "notificacoesNaoLidas": 2
}
```

### 3. Vagas Recomendadas
```http
GET /api/dashboard/vagas-recomendadas/{usuarioId}
```

### 4. Projetos Recentes
```http
GET /api/dashboard/projetos-recentes/{usuarioId}
```

### 5. Pagamentos Recentes
```http
GET /api/dashboard/pagamentos-recentes/{usuarioId}
```

## Exemplo de Uso no Angular

### Service
```typescript
// dashboard.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private apiUrl = 'http://localhost:5000/api/dashboard';

  constructor(private http: HttpClient) { }

  getDashboardResumo(usuarioId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/resumo/${usuarioId}`);
  }

  getEstatisticasRapidas(usuarioId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/estatisticas/${usuarioId}`);
  }

  getVagasRecomendadas(usuarioId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/vagas-recomendadas/${usuarioId}`);
  }
}
```

### Component
```typescript
// dashboard.component.ts
import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  dashboardData: any;
  loading = true;
  error = false;

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.carregarDashboard();
  }

  carregarDashboard(): void {
    const usuarioId = 1; // Obter do serviço de autenticação
    
    this.dashboardService.getDashboardResumo(usuarioId).subscribe({
      next: (data) => {
        this.dashboardData = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar dashboard:', error);
        this.error = true;
        this.loading = false;
      }
    });
  }
}
```

### Template
```html
<!-- dashboard.component.html -->
<div *ngIf="loading" class="loading">
  <p>Carregando dashboard...</p>
</div>

<div *ngIf="error" class="error">
  <p>Erro ao carregar dashboard. Tente novamente.</p>
</div>

<div *ngIf="!loading && !error && dashboardData" class="dashboard">
  <!-- Nível Atual -->
  <div class="card">
    <h4>Nível Atual</h4>
    <p class="nivel">{{ dashboardData.nivelNome }}</p>
    <div class="progress">
      <div class="progress-bar" [style.width.%]="dashboardData.progressoNivel"></div>
    </div>
    <p>Progresso para {{ dashboardData.proximoNivel }}</p>
  </div>

  <!-- Projetos Feitos -->
  <div class="card">
    <h4>Projetos Feitos</h4>
    <p class="valor">{{ dashboardData.totalProjetos }}</p>
    <a href="#projetos">Ver meus projetos</a>
  </div>

  <!-- Vagas Aplicadas -->
  <div class="card">
    <h4>Vagas Aplicadas</h4>
    <p class="valor">{{ dashboardData.vagasAplicadas }}</p>
    <a href="#vagas">Ver candidaturas</a>
  </div>

  <!-- Ganhos Este Mês -->
  <div class="card">
    <h4>Ganhos Este Mês</h4>
    <p class="valor">Kz {{ dashboardData.ganhosMesAtual | number:'1.0-0' }}</p>
    <a href="#ganhos">Ver histórico</a>
  </div>

  <!-- Vagas Recomendadas -->
  <div class="vagas-recomendadas">
    <h3>Vagas Recomendadas</h3>
    <div *ngFor="let vaga of dashboardData.vagasRecomendadas" class="vaga-card">
      <div class="vaga-header">
        <img [src]="apiUrl + vaga.imagemCapa" 
             [alt]="vaga.titulo"
             class="vaga-imagem"
             (error)="onImageError($event)">
        <div class="vaga-info">
          <h4>{{ vaga.titulo }}</h4>
          <p>{{ vaga.empresa }} - {{ vaga.localizacao }}</p>
          <p>{{ vaga.descricao }}</p>
        </div>
      </div>
      <div class="habilidades">
        <span *ngFor="let habilidade of vaga.habilidadesRequeridas" class="badge">
          {{ habilidade }}
        </span>
      </div>
      <span class="tempo">{{ vaga.tempoPublicacao }}</span>
      <button class="btn btn-primary">Ver Detalhes</button>
    </div>
  </div>
</div>
```

## Benefícios da Implementação

1. **Performance Otimizada**: Uma única chamada HTTP retorna todos os dados necessários
2. **Redução de Requisições**: Elimina múltiplas chamadas para diferentes endpoints
3. **Dados Consolidados**: Informações relacionadas são agrupadas logicamente
4. **Flexibilidade**: Endpoints específicos para diferentes necessidades
5. **Manutenibilidade**: Código organizado e fácil de manter

## Status Codes

- `200 OK`: Dados retornados com sucesso
- `404 Not Found`: Usuário não encontrado
- `500 Internal Server Error`: Erro interno do servidor

## Observações

- O endpoint principal `/resumo/{usuarioId}` retorna todos os dados necessários para o dashboard
- Os endpoints específicos são úteis para atualizações parciais ou carregamento sob demanda
- Os dados são calculados em tempo real baseados nas informações do banco de dados
- O sistema de vagas recomendadas é baseado nas habilidades do usuário 