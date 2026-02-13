import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConfig } from '../config/api.config';

export interface DashboardResumo {
  usuarioId: number;
  nome: string;
  email: string;
  fotoPerfil: string;
  nivelAtual: number;
  nivelNome: string;
  progressoNivel: number;
  proximoNivel: string;
  totalProjetos: number;
  projetosConcluidos: number;
  projetosEmAndamento: number;
  vagasAplicadas: number;
  ganhosMesAtual: number;
  ganhosMesAnterior: number;
  testesRealizados: number;
  notificacoesNaoLidas: number;
  cv: string;
  habilidades: string;
  habilidadesLista: string[];
  certificacoes: Array<{
    nome: string;
    ano: number;
  }>;
  vagasRecomendadas: Array<{
    id: number;
    titulo: string;
    empresa: string;
    localizacao: string;
    descricao: string;
    habilidadesRequeridas: string[];
    dataPublicacao: string;
    tempoPublicacao: string;
    imagemCapa: string;
  }>;
  projetosRecentes: Array<{
    id: number;
    descricao: string;
    tipo: string;
    status: number;
    dataInicio: string;
    dataConclusao: string;
    valor: number;
    imagemCapa: string;
  }>;
  pagamentosRecentes: Array<{
    id: number;
    valor: number;
    data: string;
    status: number;
    empresa: string;
  }>;
}

export interface EstatisticasRapidas {
  nivelAtual: string;
  progressoNivel: number;
  proximoNivel: string;
  totalProjetos: number;
  projetosConcluidos: number;
  vagasAplicadas: number;
  ganhosMesAtual: number;
  testesRealizados: number;
  notificacoesNaoLidas: number;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient) { }

  getDashboardResumo(usuarioId: number): Observable<DashboardResumo> {
    return this.http.get<DashboardResumo>(ApiConfig.DASHBOARD.RESUMO(usuarioId));
  }

  getEstatisticasRapidas(usuarioId: number): Observable<EstatisticasRapidas> {
    return this.http.get<EstatisticasRapidas>(ApiConfig.DASHBOARD.ESTATISTICAS(usuarioId));
  }

  getVagasRecomendadas(usuarioId: number): Observable<any[]> {
    return this.http.get<any[]>(ApiConfig.DASHBOARD.VAGAS_RECOMENDADAS(usuarioId));
  }

  getProjetosRecentes(usuarioId: number): Observable<any[]> {
    return this.http.get<any[]>(ApiConfig.DASHBOARD.PROJETOS_RECENTES(usuarioId));
  }

  getPagamentosRecentes(usuarioId: number): Observable<any[]> {
    return this.http.get<any[]>(ApiConfig.DASHBOARD.PAGAMENTOS_RECENTES(usuarioId));
  }
} 