import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface HeaderJovem {
  usuarioId: number;
  nome: string;
  saudacao: string;
  nivelNome: string;
  badgeNivel: string;
  fotoPerfil: string;
  notificacoesNaoLidas: number;
  mensagensNaoLidas: number;
  forumNaoLidas: number;
  fraseMotivacional: string;
}

@Injectable({ providedIn: 'root' })
export class HeaderService {
  private apiUrl = environment.apiUrl + '/Header/jovem';

  constructor(private http: HttpClient) {}

  getHeaderJovem(usuarioId: number): Observable<HeaderJovem> {
    return this.http.get<HeaderJovem>(`${this.apiUrl}/${usuarioId}`);
  }
} 