import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule], // <- IMPORTANTE
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  senha = '';

  constructor(private auth: AuthService, private router: Router) {}

  onSubmit(form: any) {
    if (form.valid) {
      this.auth.login(this.email, this.senha).subscribe({
        next: (res) => {
          this.auth.setUserData(res);
          Swal.fire({
            icon: 'success',
            title: 'Login realizado com sucesso!',
            confirmButtonColor: '#b4872d'
          }).then(() => {
            this.email = '';
            this.senha = '';
            // Redireciona conforme o tipo de usuário
            const tipo = res.tipoUsuario;

            if (tipo === 'Jovem' || tipo === 0) {
              // Estudante: vai para o dashboard padrão
              this.router.navigate(['/page/dashboard']);
            } else if (tipo === 'Empresa' || tipo === 1) {
              // Empresa: vai direto para "meus projetos"
              this.router.navigate(['/page/meus-projetos']);
            } else if (tipo === 'Gestor' || tipo === 2) {
              // Gestor: usa o dashboard, mas com blocos específicos no front
              this.router.navigate(['/page/dashboard'], { queryParams: { gestor: true } });
            } else {
              // Fallback
              this.router.navigate(['/page/dashboard']);
            }
          });
        },
        error: () => {
          Swal.fire({
            icon: 'error',
            title: 'Erro ao fazer login',
            text: 'E-mail ou senha inválidos.',
            confirmButtonColor: '#b4872d'
          });
        }
      });
    }
  }

  goToHome() {
    this.router.navigate(['/']);
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }
}
