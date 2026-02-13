import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ApiConfig } from '../../../config/api.config';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class SidebarComponent implements OnInit {
  user: any;
  isSidebarActive: boolean = false;
  userData: any = null; // Dados completos do usuário da API

  constructor(
    private auth: AuthService, 
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.user = this.auth.getUserData();
    if (this.user && this.user.id) {
      this.carregarDadosUsuario();
    }
  }

  carregarDadosUsuario() {
    this.http.get<any>(`${ApiConfig.BASE_URL}/usuarios/${this.user.id}`).subscribe({
      next: (data) => {
        this.userData = data;
        // Atualizar dados do usuário no auth service se necessário
        if (data) {
          this.user = { ...this.user, ...data };
        }
      },
      error: (err) => {
        console.error('Erro ao carregar dados do usuário:', err);
        // Usar dados do auth service como fallback
        this.userData = this.user;
      }
    });
  }

  toggleSidebar(): void {
    this.isSidebarActive = !this.isSidebarActive;
  }

  logout(): void {
    this.auth.logout();
    this.user = null;
    this.userData = null;
    this.router.navigate(['/login']);
  }

  get nomeUsuario(): string {
    return this.userData?.nome || this.user?.nome || 'Usuário';
  }

  get emailUsuario(): string {
    return this.userData?.email || this.user?.email || '';
  }

  get fotoPerfil(): string {
    return this.userData?.fotoPerfil || this.user?.fotoPerfil || '';
  }
}
