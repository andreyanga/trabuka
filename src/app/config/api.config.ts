import { environment } from '../../environments/environment';

export class ApiConfig {
  // Base URL da API
  static readonly BASE_URL = environment.apiUrl;
  
  // Base URL para recursos estáticos (imagens, etc.)
  static readonly STATIC_BASE_URL = environment.apiUrl.replace('/api', '');
  
  // Endpoints do Dashboard
  static readonly DASHBOARD = {
    RESUMO: (usuarioId: number) => `${this.BASE_URL}/Dashboard/resumo/${usuarioId}`,
    ESTATISTICAS: (usuarioId: number) => `${this.BASE_URL}/Dashboard/estatisticas/${usuarioId}`,
    VAGAS_RECOMENDADAS: (usuarioId: number) => `${this.BASE_URL}/Dashboard/vagas-recomendadas/${usuarioId}`,
    PROJETOS_RECENTES: (usuarioId: number) => `${this.BASE_URL}/Dashboard/projetos-recentes/${usuarioId}`,
    PAGAMENTOS_RECENTES: (usuarioId: number) => `${this.BASE_URL}/Dashboard/pagamentos-recentes/${usuarioId}`
  };
  
  // Endpoints de Autenticação
  static readonly AUTH = {
    LOGIN: `${this.STATIC_BASE_URL}/Auth/login`
  };
  
  // Endpoints de Testes
  static readonly TESTE = {
    LISTAR: `${this.BASE_URL}/Teste`,
    DISPONIVEIS: (usuarioId: number) => `${this.BASE_URL}/Teste/disponiveis/${usuarioId}`,
    POR_ID: (id: number) => `${this.BASE_URL}/Teste/${id}`,
    INICIAR: (testeId: number) => `${this.BASE_URL}/Teste/${testeId}/iniciar`,
    SUBMETER: `${this.BASE_URL}/Teste/submeter`
  };
  
  // Endpoints de Candidaturas
  static readonly CANDIDATURAS = {
    CRIAR: `${this.BASE_URL}/Candidaturas`,
    PENDENTES: `${this.BASE_URL}/Candidaturas/pendentes`,
    APROVAR: (id: number) => `${this.BASE_URL}/Candidaturas/${id}/aprovar`,
    REJEITAR: (id: number) => `${this.BASE_URL}/Candidaturas/${id}/rejeitar`
  };
  
  // Método para construir URL de imagem
  static getImageUrl(imagePath: string): string {
    if (!imagePath) {
      return '../../../../assets/img/about.jpg';
    }
    
    if (imagePath.startsWith('http')) {
      return imagePath;
    }
    
    return `${this.STATIC_BASE_URL}/${imagePath}`;
  }
} 