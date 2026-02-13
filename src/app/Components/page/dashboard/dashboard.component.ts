import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { DashboardService, DashboardResumo } from '../../../services/dashboard.service';
import { DashboardMockService } from '../../../services/dashboard-mock.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { ApiConfig } from '../../../config/api.config';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  user: any;
  dashboardData: DashboardResumo | null = null;
  loading = true;
  error = false;
  errorMessage = '';
  usingMockData = false;
  isGestor = false;
  usuariosPendentes: any[] = [];
  projetosPendentes: any[] = [];
  relatoriosPendentes: any[] = [];
  candidaturasPendentes: any[] = [];
  
  // Dados para Empresa
  empresaData: any = null;
  projetosEmpresa: any[] = [];
  
  // Dados para Gestor
  gestorStats: any = {
    usuariosPendentes: 0,
    projetosPendentes: 0,
    transacoesPendentes: 0,
    receitaMes: 0
  };

  constructor(
    private auth: AuthService,
    private dashboardService: DashboardService,
    private dashboardMockService: DashboardMockService,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.user = this.auth.getUserData();
    this.isGestor = this.user && (this.user.tipoUsuario === 'Gestor' || this.user.tipoUsuario === 2);
    if (this.user && this.user.id) {
      if (this.user.tipoUsuario === 0) {
        // Estudante/Jovem
        this.carregarDashboard();
      } else if (this.user.tipoUsuario === 1) {
        // Empresa
        this.carregarDashboardEmpresa();
      } else if (this.isGestor) {
        // Gestor
        this.carregarDashboardGestor();
      } else {
        this.loading = false;
      }
    } else {
      this.loading = false;
      this.error = true;
      this.errorMessage = 'Usuário não autenticado';
    }
  }

  carregarDashboard(): void {
    this.loading = true;
    this.error = false;
    this.usingMockData = false;
    
    // Verifica se deve forçar o uso de dados mock
    if (environment.useMockData) {
      console.log('Usando dados mock forçados pela configuração');
      this.usingMockData = true;
      this.carregarDashboardMock();
      return;
    }
    
    // Carrega dados diretamente da API
    this.carregarDashboardReal();
  }

  carregarDashboardReal(): void {
    this.dashboardService.getDashboardResumo(this.user.id).subscribe({
      next: (data) => {
        this.dashboardData = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar dados da API:', error);
        this.usingMockData = true;
        this.carregarDashboardMock();
      }
    });
  }

  carregarDashboardMock(): void {
    this.dashboardMockService.getDashboardResumo(this.user.id).subscribe({
      next: (data) => {
        this.dashboardData = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar dashboard mock:', error);
        this.error = true;
        this.errorMessage = 'Erro ao carregar dados do dashboard. Tente novamente.';
        this.loading = false;
      }
    });
  }

  getStatusProjeto(status: number): string {
    switch (status) {
      case 1: return 'Em Andamento';
      case 2: return 'Concluído';
      case 3: return 'Cancelado';
      default: return 'Desconhecido';
    }
  }

  getStatusPagamento(status: number): string {
    switch (status) {
      case 1: return 'Pendente';
      case 2: return 'Pago';
      case 3: return 'Cancelado';
      default: return 'Desconhecido';
    }
  }

  getStatusClass(status: number): string {
    switch (status) {
      case 1: return 'status-pendente';
      case 2: return 'status-concluido';
      case 3: return 'status-cancelado';
      default: return '';
    }
  }

  formatarMoeda(valor: number): string {
    return `Kz ${valor.toLocaleString('pt-AO')}`;
  }

  formatarData(data: string): string {
    return new Date(data).toLocaleDateString('pt-AO');
  }

  getFotoPerfilUrl(fotoPerfil: string): string {
    return ApiConfig.getImageUrl(fotoPerfil);
  }

  carregarPendenciasGestor() {
    // estudantes pendentes - StatusUsuario.Pendente = 2 (Ativo=0, Inativo=1, Pendente=2)
    // Tentar primeiro com número, se falhar tentar com string
    this.http.get<any[]>(`${ApiConfig.BASE_URL}/usuarios/status/2`).subscribe({
      next: (r) => {
        this.usuariosPendentes = r || [];
        this.gestorStats.usuariosPendentes = this.usuariosPendentes.length;
      },
      error: (err) => {
        console.error('Erro ao carregar usuários pendentes:', err);
        this.usuariosPendentes = [];
      }
    });

    // projetos pendentes - StatusProjeto.Pendente = 0
    this.http.get<any[]>(`${ApiConfig.BASE_URL}/projetos/status/0`).subscribe({
      next: (r) => {
        this.projetosPendentes = r || [];
        this.gestorStats.projetosPendentes = this.projetosPendentes.length;
      },
      error: (err) => {
        console.error('Erro ao carregar projetos pendentes:', err);
        this.projetosPendentes = [];
      }
    });

    // relatórios pendentes - StatusRelatorio.Pendente = 0
    this.http.get<any[]>(`${ApiConfig.BASE_URL}/Relatorio/status/0`).subscribe({
      next: (r) => {
        this.relatoriosPendentes = r || [];
      },
      error: (err) => {
        console.error('Erro ao carregar relatórios pendentes:', err);
        this.relatoriosPendentes = [];
      }
    });

    // candidaturas pendentes - StatusCandidatura.Pendente = 0
    this.http.get<any[]>(ApiConfig.CANDIDATURAS.PENDENTES).subscribe({
      next: (r) => {
        this.candidaturasPendentes = r || [];
      },
      error: (err) => {
        console.error('Erro ao carregar candidaturas pendentes:', err);
        this.candidaturasPendentes = [];
      }
    });
  }

  aprovarUsuario(id: number) {
    this.http.patch(`${ApiConfig.BASE_URL}/usuarios/${id}/aprovar`, {}).subscribe({
      next: () => {
        this.usuariosPendentes = this.usuariosPendentes.filter(u => (u.id !== id && u.usuarioId !== id));
        this.gestorStats.usuariosPendentes = this.usuariosPendentes.length;
        // Recarregar dados
        this.carregarPendenciasGestor();
      },
      error: (err) => {
        console.error('Erro ao aprovar usuário:', err);
      }
    });
  }

  aprovarProjeto(id: number) {
    this.http.patch(`${ApiConfig.BASE_URL}/projetos/${id}/aprovar`, {}).subscribe({
      next: () => {
        this.projetosPendentes = this.projetosPendentes.filter(p => (p.id !== id && p.projetoId !== id));
        this.gestorStats.projetosPendentes = this.projetosPendentes.length;
        // Recarregar dados
        this.carregarPendenciasGestor();
      },
      error: (err) => {
        console.error('Erro ao aprovar projeto:', err);
      }
    });
  }

  aprovarCandidatura(id: number) {
    this.http.put(ApiConfig.CANDIDATURAS.APROVAR(id), {}).subscribe({
      next: () => {
        this.candidaturasPendentes = this.candidaturasPendentes.filter(c => c.id !== id);
        // Recarregar dados
        this.carregarPendenciasGestor();
      },
      error: (err) => {
        console.error('Erro ao aprovar candidatura:', err);
      }
    });
  }

  avaliarRelatorio(relatorio: any, notaStr: string, feedback: string) {
    const nota = Number(notaStr);
    if (isNaN(nota) || nota < 0 || nota > 10) {
      alert('Nota deve ser um número entre 0 e 10');
      return;
    }

    if (!feedback || feedback.trim() === '') {
      alert('Feedback é obrigatório');
      return;
    }

    const body = { nota, feedback };

    this.http.post(`${ApiConfig.BASE_URL}/Relatorio/${relatorio.id}/avaliar`, body).subscribe({
      next: () => {
        this.relatoriosPendentes = this.relatoriosPendentes.filter(r => r.id !== relatorio.id);
        // Recarregar dados
        this.carregarPendenciasGestor();
      },
      error: (err) => {
        console.error('Erro ao avaliar relatório:', err);
        alert('Erro ao avaliar relatório. Tente novamente.');
      }
    });
  }

  carregarDashboardEmpresa() {
    this.loading = true;
    this.error = false;
    
    // Buscar empresa pelo email do usuário logado
    this.http.get<any[]>(`${ApiConfig.BASE_URL}/empresas`).subscribe({
      next: (empresas) => {
        // Encontrar empresa pelo email
        const empresa = empresas.find(e => e.email === this.user.email);
        if (empresa) {
          this.empresaData = empresa;
          this.carregarProjetosEmpresa();
        } else {
          // Se não encontrar, usar dados do usuário
          this.empresaData = {
            nome: this.user.nome,
            email: this.user.email,
            setor: 'Não especificado',
            contato: this.user.telefone || 'Não disponível'
          };
          this.carregarProjetosEmpresa();
        }
      },
      error: (err) => {
        console.error('Erro ao carregar dados da empresa:', err);
        // Usar dados do usuário como fallback
        this.empresaData = {
          nome: this.user.nome,
          email: this.user.email,
          setor: 'Não especificado',
          contato: this.user.telefone || 'Não disponível'
        };
        this.carregarProjetosEmpresa();
      }
    });
  }

  carregarProjetosEmpresa() {
    // Buscar projetos pela empresaId
    const empresaId = this.empresaData?.empresaId || this.empresaData?.id;
    
    if (empresaId) {
      this.http.get<any[]>(`${ApiConfig.BASE_URL}/projetos/empresa/${empresaId}`).subscribe({
        next: (projetos) => {
          this.projetosEmpresa = projetos;
          this.loading = false;
        },
        error: (err) => {
          console.error('Erro ao carregar projetos:', err);
          this.projetosEmpresa = [];
          this.loading = false;
        }
      });
    } else {
      // Se não encontrar empresaId, tentar buscar todos os projetos e filtrar
      this.http.get<any[]>(`${ApiConfig.BASE_URL}/projetos`).subscribe({
        next: (projetos) => {
          // Filtrar projetos que podem pertencer à empresa (se tiverem empresaId ou email correspondente)
          this.projetosEmpresa = projetos || [];
          this.loading = false;
        },
        error: (err) => {
          console.error('Erro ao carregar projetos:', err);
          this.projetosEmpresa = [];
          this.loading = false;
        }
      });
    }
  }

  carregarDashboardGestor() {
    this.loading = true;
    this.error = false;
    
    // Carregar todas as pendências
    this.carregarPendenciasGestor();
    
    // Calcular receita (simplificado - você pode criar um endpoint específico)
    this.http.get<any[]>(`${ApiConfig.BASE_URL}/pagamentos`).subscribe({
      next: (pagamentos) => {
        const mesAtual = new Date().getMonth();
        const receitaMes = pagamentos
          .filter(p => {
            const dataPagamento = new Date(p.data);
            return dataPagamento.getMonth() === mesAtual && p.status === 2;
          })
          .reduce((sum, p) => sum + (p.valor * 0.2), 0); // 20% de comissão
        this.gestorStats.receitaMes = receitaMes;
        this.loading = false;
      },
      error: () => {
        this.gestorStats.receitaMes = 0;
        this.loading = false;
      }
    });
  }

  // Getters para contagens de projetos da empresa
  get vagasAtivas(): number {
    if (!this.projetosEmpresa || this.projetosEmpresa.length === 0) return 0;
    return this.projetosEmpresa.filter(p => p.status === 1).length;
  }

  get projetosPendentesEmpresa(): number {
    if (!this.projetosEmpresa || this.projetosEmpresa.length === 0) return 0;
    return this.projetosEmpresa.filter(p => p.status === 0).length;
  }

  get projetosConcluidosEmpresa(): number {
    if (!this.projetosEmpresa || this.projetosEmpresa.length === 0) return 0;
    return this.projetosEmpresa.filter(p => p.status === 2).length;
  }
}
