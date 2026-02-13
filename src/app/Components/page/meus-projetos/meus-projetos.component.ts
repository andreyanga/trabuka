import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../../services/auth.service';
import { ApiConfig } from '../../../config/api.config';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-meus-projetos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './meus-projetos.component.html',
  styleUrl: './meus-projetos.component.css'
})
export class MeusProjetosComponent implements OnInit {
  projetos: any[] = [];
  loading = true;
  error = false;
  errorMessage = '';

  // campos do formulário de nova vaga
  novaDescricao = '';
  novaTipo = 'Inicial';
  novoOrcamento: number | null = null;
  novoPrazo: string = '';

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const user = this.authService.getUserData();

    if (!user || !user.id) {
      this.error = true;
      this.errorMessage = 'Usuário não autenticado.';
      this.loading = false;
      return;
    }

    if (this.isEmpresa) {
      this.carregarProjetosEmpresa(user.id);
    } else {
      this.carregarProjetosRecentesUsuario(user.id);
    }
  }

  get isEmpresa(): boolean {
    const user = this.authService.getUserData();
    return user && (user.tipoUsuario === 'Empresa' || user.tipoUsuario === 1);
  }

  carregarProjetosEmpresa(empresaId: number) {
    this.http.get<any[]>(`${ApiConfig.BASE_URL}/projetos/empresa/${empresaId}`).subscribe({
      next: (data) => {
        this.projetos = data;
        this.loading = false;
      },
      error: () => {
        this.error = true;
        this.errorMessage = 'Não foi possível carregar os projetos da empresa.';
        this.loading = false;
      }
    });
  }

  carregarProjetosRecentesUsuario(usuarioId: number) {
    this.http.get<any[]>(ApiConfig.DASHBOARD.PROJETOS_RECENTES(usuarioId)).subscribe({
      next: (data) => {
        this.projetos = data;
        this.loading = false;
      },
      error: () => {
        this.error = true;
        this.errorMessage = 'Não foi possível carregar os seus projetos.';
        this.loading = false;
      }
    });
  }

  publicarVaga() {
    const user = this.authService.getUserData();
    if (!user || !user.id) return;
    // somente empresas podem publicar vagas
    if (!this.isEmpresa) {
      this.error = true;
      this.errorMessage = 'Apenas contas do tipo Empresa podem publicar vagas.';
      return;
    }

    const body = {
      descricao: this.novaDescricao,
      tipo: this.novaTipo,
      oramento: this.novoOrcamento ?? 0,
      horario: new Date().toISOString(),
      prazo: this.novoPrazo || new Date().toISOString(),
      // garantir que empresaId é o id do user logado (tipo Empresa)
      empresaId: user.id,
      mentorId: null,
      status: 0, // StatusProjeto.Pendente
      imagemCapa: null
    };

    // validações básicas antes de enviar
    if (!body.descricao || body.descricao.trim().length === 0) {
      this.error = true;
      this.errorMessage = 'Descrição é obrigatória.';
      return;
    }
    if (!body.oramento || body.oramento <= 0) {
      this.error = true;
      this.errorMessage = 'Orçamento deve ser maior que zero.';
      return;
    }
    const prazoDate = new Date(body.prazo);
    if (isNaN(prazoDate.getTime()) || prazoDate <= new Date()) {
      this.error = true;
      this.errorMessage = 'Prazo inválido (deve ser uma data futura).';
      return;
    }

    // use the JSON endpoint that accepts project creation without file upload
    this.error = false;
    this.errorMessage = '';
    this.http.post<any>(`${ApiConfig.BASE_URL}/projetos/create-json`, body).subscribe({
      next: (projeto) => {
        this.projetos.push(projeto);
        this.novaDescricao = '';
        this.novaTipo = 'Inicial';
        this.novoOrcamento = null;
        this.novoPrazo = '';
      },
      error: () => {
        this.error = true;
        this.errorMessage = 'Não foi possível publicar a vaga.';
      }
    });
  }
}
