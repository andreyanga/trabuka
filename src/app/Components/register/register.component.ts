import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  nome = '';
  email = '';
  senha = '';
  tipoUsuario: number | null = null; // 0 = Jovem, 1 = Empresa
  telefone = '';
  concordaTermos = false;

  isFormVisible = false;
  role: 'jovem' | 'empresa' | null = null;

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private router: Router) {}

  selectRole(role: 'jovem' | 'empresa') {
    this.role = role;
    this.tipoUsuario = role === 'jovem' ? 0 : 1;
  }

  showForm() {
    if (this.role) {
      this.isFormVisible = true;
    }
  }

  goTologin() {
    this.router.navigate(['/login']);
  }

  goToHome() {
    this.router.navigate(['/']);
  }

  limparCampos() {
    this.nome = '';
    this.email = '';
    this.senha = '';
    this.tipoUsuario = null;
    this.telefone = '';
    this.concordaTermos = false;
    this.role = null;
    this.isFormVisible = false;
  }

  onSubmit(form: any) {
    if (form.valid && this.concordaTermos) {
      const formData = new FormData();
      formData.append('Nome', this.nome);
      formData.append('Email', this.email);
      formData.append('Senha', this.senha);
      formData.append('TipoUsuario', String(this.tipoUsuario));
      formData.append('Telefone', this.telefone);

      this.http.post(`${this.apiUrl}/Usuarios`, formData).subscribe({
        next: () => {
          Swal.fire({
            icon: 'success',
            title: 'Cadastro realizado com sucesso!',
            text: 'Clique em OK para ir para a tela de login.',
            confirmButtonColor: '#b4872d'
          }).then(() => {
            this.limparCampos();
            this.router.navigate(['/login']);
          });
        },
        error: () => {
          Swal.fire({
            icon: 'error',
            title: 'Erro ao cadastrar',
            text: 'Verifique os dados e tente novamente.',
            confirmButtonColor: '#b4872d'
          });
        }
      });
    }
  }
}
