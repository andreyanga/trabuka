import { Injectable } from '@angular/core';
import { Observable, of, delay } from 'rxjs';
import { DashboardResumo, EstatisticasRapidas } from './dashboard.service';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardMockService {
  
  private mockDashboardData: DashboardResumo = {
    usuarioId: 1,
    nome: "Andre Yanga",
    email: "andreyanga@gmail.com",
    fotoPerfil: "../../../../assets/img/about.jpg",
    nivelAtual: 2,
    nivelNome: "Praticante",
    progressoNivel: 75,
    proximoNivel: "Mestre",
    totalProjetos: 2,
    projetosConcluidos: 1,
    projetosEmAndamento: 1,
    vagasAplicadas: 5,
    ganhosMesAtual: 35000.00,
    ganhosMesAnterior: 28000.00,
    testesRealizados: 3,
    notificacoesNaoLidas: 2,
    cv: "Estudante de Engenharia Informática apaixonado por desenvolvimento web front-end. Buscando oportunidades para aplicar meus conhecimentos em HTML, CSS, JavaScript e frameworks modernos.",
    habilidades: "HTML5, CSS3, JavaScript, React, Figma",
    habilidadesLista: ["HTML5", "CSS3", "JavaScript", "React", "Figma"],
    certificacoes: [
      {
        nome: "Certificação Front-End Developer - Plataforma X",
        ano: 2023
      },
      {
        nome: "UI/UX Design Fundamentos - Escola Y",
        ano: 2022
      }
    ],
    vagasRecomendadas: [
      {
        id: 1,
        titulo: "Estágio em Desenvolvimento Front-End",
        empresa: "Tech Solutions Inc.",
        localizacao: "Luanda",
        descricao: "Buscamos um estudante proativo para integrar nossa equipe de desenvolvimento. Oportunidade de aprendizado em projetos reais com React e Node.js.",
        habilidadesRequeridas: ["HTML", "CSS", "JavaScript"],
        dataPublicacao: "2024-01-15T10:00:00",
        tempoPublicacao: "há 2 dias",
        imagemCapa: "/assets/images/projetos/portfolio-1.jpg"
      },
      {
        id: 2,
        titulo: "Trainee UI/UX Designer",
        empresa: "Agência Criativa Digital",
        localizacao: "Remoto",
        descricao: "Programa de trainee para jovens designers com paixão por interfaces intuitivas e experiência do usuário. Conhecimento em Figma é um diferencial.",
        habilidadesRequeridas: ["Figma", "UI Design", "UX Research"],
        dataPublicacao: "2024-01-10T10:00:00",
        tempoPublicacao: "há 5 dias",
        imagemCapa: "/assets/images/projetos/portfolio-2.jpg"
      }
    ],
    projetosRecentes: [
      {
        id: 1,
        descricao: "Sistema de Gestão Escolar",
        tipo: "Web",
        status: 1,
        dataInicio: "2024-01-15T10:00:00",
        dataConclusao: "2024-07-15T10:00:00",
        valor: 50000.00,
        imagemCapa: "/assets/images/projetos/portfolio-3.jpg"
      },
      {
        id: 2,
        descricao: "Aplicativo Mobile de Delivery",
        tipo: "Mobile",
        status: 2,
        dataInicio: "2023-11-01T10:00:00",
        dataConclusao: "2024-01-10T10:00:00",
        valor: 75000.00,
        imagemCapa: "/assets/images/projetos/portfolio-4.jpg"
      }
    ],
    pagamentosRecentes: [
      {
        id: 1,
        valor: 25000.00,
        data: "2024-01-20T10:00:00",
        status: 2,
        empresa: "Tech Solutions Inc."
      },
      {
        id: 2,
        valor: 10000.00,
        data: "2024-01-15T10:00:00",
        status: 2,
        empresa: "Agência Criativa Digital"
      }
    ]
  };

  getDashboardResumo(usuarioId: number): Observable<DashboardResumo> {
    // Simula um delay de rede
    return of(this.mockDashboardData).pipe(delay(environment.mockDelay || 1000));
  }

  getEstatisticasRapidas(usuarioId: number): Observable<EstatisticasRapidas> {
    const estatisticas: EstatisticasRapidas = {
      nivelAtual: this.mockDashboardData.nivelNome,
      progressoNivel: this.mockDashboardData.progressoNivel,
      proximoNivel: this.mockDashboardData.proximoNivel,
      totalProjetos: this.mockDashboardData.totalProjetos,
      projetosConcluidos: this.mockDashboardData.projetosConcluidos,
      vagasAplicadas: this.mockDashboardData.vagasAplicadas,
      ganhosMesAtual: this.mockDashboardData.ganhosMesAtual,
      testesRealizados: this.mockDashboardData.testesRealizados,
      notificacoesNaoLidas: this.mockDashboardData.notificacoesNaoLidas
    };
    
    return of(estatisticas).pipe(delay(500));
  }

  getVagasRecomendadas(usuarioId: number): Observable<any[]> {
    return of(this.mockDashboardData.vagasRecomendadas).pipe(delay(800));
  }

  getProjetosRecentes(usuarioId: number): Observable<any[]> {
    return of(this.mockDashboardData.projetosRecentes).pipe(delay(600));
  }

  getPagamentosRecentes(usuarioId: number): Observable<any[]> {
    return of(this.mockDashboardData.pagamentosRecentes).pipe(delay(400));
  }
} 