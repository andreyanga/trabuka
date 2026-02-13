import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { HeaderService, HeaderJovem } from '../../../services/header.service';
import { CommonModule } from '@angular/common';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiConfig } from '../../../config/api.config';

@Component({
  selector: 'app-header-layout',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header-layout.component.html',
  styleUrls: ['./header-layout.component.css']
})
export class HeaderLayoutComponent implements OnInit {
  user: any;
  headerData: HeaderJovem | null = null;
  loading = true;
  error = false;
  
  // Dados para empresa
  empresaData: any = null;
  
  // Dados para gestor/suporte
  notificacoesNaoLidas = 0;
  mensagensNaoLidas = 0;

  constructor(
    private auth: AuthService, 
    private headerService: HeaderService,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.user = this.auth.getUserData();
    if (this.user && this.user.id) {
      if (this.user.tipoUsuario === 0) {
        // Estudante/Jovem
        this.headerService.getHeaderJovem(this.user.id).subscribe({
          next: (data) => {
            this.headerData = data;
            this.loading = false;
          },
          error: () => {
            this.error = true;
            this.loading = false;
          }
        });
      } else if (this.user.tipoUsuario === 1) {
        // Empresa
        this.carregarDadosEmpresa();
      } else {
        // Gestor ou Suporte
        this.carregarDadosGestorSuporte();
      }
    } else {
      this.loading = false;
      this.error = true;
    }
  }

  carregarDadosEmpresa() {
    this.http.get<any[]>(`${ApiConfig.BASE_URL}/empresas`).subscribe({
      next: (empresas) => {
        const empresa = empresas.find(e => e.email === this.user.email);
        if (empresa) {
          this.empresaData = empresa;
        } else {
          this.empresaData = {
            nome: this.user.nome,
            email: this.user.email
          };
        }
        this.carregarNotificacoes();
      },
      error: () => {
        this.empresaData = {
          nome: this.user.nome,
          email: this.user.email
        };
        this.loading = false;
      }
    });
  }

  carregarDadosGestorSuporte() {
    // Carregar notificações não lidas
    this.carregarNotificacoes();
  }

  carregarNotificacoes() {
    this.http.get<any[]>(`${ApiConfig.BASE_URL}/notificacoes/usuario/${this.user.id}`).subscribe({
      next: (notificacoes) => {
        this.notificacoesNaoLidas = notificacoes.filter(n => !n.lida).length;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  getFotoPerfilUrl(fotoPerfil: string): string {
    if (!fotoPerfil) {
      // fallback para imagem padrão existente (caso não haja default.jpg)
      return `${environment.imageBaseUrl}/assets/images/usuarios/09f39422-7268-4f80-962f-1f62d249343a.jpg`;
    }
    if (fotoPerfil.startsWith('http')) {
      return fotoPerfil;
    }
    return `${environment.imageBaseUrl}/${fotoPerfil.startsWith('/') ? fotoPerfil.slice(1) : fotoPerfil}`;
  }
  
}
