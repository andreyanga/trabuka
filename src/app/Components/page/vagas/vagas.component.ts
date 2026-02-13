import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardService, DashboardResumo } from '../../../services/dashboard.service';
import { AuthService } from '../../../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { ApiConfig } from '../../../config/api.config';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-vagas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './vagas.component.html',
  styleUrl: './vagas.component.css'
})
export class VagasComponent implements OnInit {
  dashboardData: DashboardResumo | null = null;
  loading = true;
  error = false;
  errorMessage = '';
  user: any;
  candidaturasEnviadas = new Set<number>();

  constructor(
    private dashboardService: DashboardService,
    private authService: AuthService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.user = this.authService.getUserData();
    if (!this.user || !this.user.id) {
      this.error = true;
      this.errorMessage = 'Usuário não autenticado.';
      this.loading = false;
      return;
    }

    this.dashboardService.getDashboardResumo(this.user.id).subscribe({
      next: (data) => {
        this.dashboardData = data;
        this.loading = false;
      },
      error: () => {
        this.error = true;
        this.errorMessage = 'Não foi possível carregar as vagas recomendadas.';
        this.loading = false;
      }
    });
  }

  get vagasRecomendadas() {
    return this.dashboardData?.vagasRecomendadas || [];
  }

  candidatar(vaga: any) {
    if (this.candidaturasEnviadas.has(vaga.id)) {
      return;
    }

    this.http.post(ApiConfig.CANDIDATURAS.CRIAR, {
      usuarioId: this.user.id,
      projetoId: vaga.id
    }).subscribe({
      next: () => {
        this.candidaturasEnviadas.add(vaga.id);
        Swal.fire('Candidatura enviada!', 'Agora o gestor irá analisar.', 'success');
      },
      error: (err) => {
        Swal.fire('Erro', err.error || 'Não foi possível enviar a candidatura.', 'error');
      }
    });
  }

  jaCandidatado(vaga: any): boolean {
    return this.candidaturasEnviadas.has(vaga.id);
  }
}
