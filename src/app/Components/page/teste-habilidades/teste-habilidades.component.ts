import { Component, OnInit, OnDestroy } from '@angular/core';
import { TesteService, TesteReadDto, TesteComQuestoesDto, SubmeterTesteDto, ResultadoTesteCompletoDto, RespostaQuestaoDto } from '../../../services/teste.service';
import { AuthService } from '../../../services/auth.service';
import Swal from 'sweetalert2';

enum EstadoTeste {
  LISTA = 'lista',
  INSTRUCOES = 'instrucoes',
  EM_PROGRESSO = 'em_progresso',
  RESULTADO = 'resultado'
}

@Component({
  selector: 'app-teste-habilidades',
  standalone: false,
  templateUrl: './teste-habilidades.component.html',
  styleUrl: './teste-habilidades.component.css'
})
export class TesteHabilidadesComponent implements OnInit, OnDestroy {
  estado: EstadoTeste = EstadoTeste.LISTA;
  testesDisponiveis: TesteReadDto[] = [];
  testeAtual: TesteComQuestoesDto | null = null;
  resultado: ResultadoTesteCompletoDto | null = null;
  respostas: Map<number, string> = new Map(); // Map<questaoId, resposta>
  
  // Timer
  tempoRestante: number = 0; // em segundos
  timerInterval: any;
  tempoFormatado: string = '00:00';
  
  // Progresso
  questaoAtual: number = 0;
  usuarioId: number = 0;
  dataInicio: Date | null = null;
  carregando: boolean = false;

  constructor(
    private testeService: TesteService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    const userData = this.authService.getUserData();
    this.usuarioId = userData.usuarioId || 1; // Fallback para teste
    
    this.carregarTestesDisponiveis();
    window.scrollTo(0, 0);
  }

  ngOnDestroy() {
    this.pararTimer();
  }

  carregarTestesDisponiveis() {
    this.carregando = true;
    this.testeService.getTestesDisponiveis(this.usuarioId).subscribe({
      next: (testes) => {
        this.testesDisponiveis = testes;
        this.carregando = false;
      },
      error: (error) => {
        console.error('Erro ao carregar testes:', error);
        Swal.fire('Erro', 'Não foi possível carregar os testes disponíveis.', 'error');
        this.carregando = false;
      }
    });
  }

  verInstrucoes(teste: TesteReadDto) {
    this.carregando = true;
    this.testeService.iniciarTeste(teste.testeId, this.usuarioId).subscribe({
      next: (testeComQuestoes) => {
        this.testeAtual = testeComQuestoes;
        this.dataInicio = new Date();
        this.estado = EstadoTeste.INSTRUCOES;
        this.carregando = false;
      },
      error: (error) => {
        console.error('Erro ao iniciar teste:', error);
        Swal.fire('Erro', error.error?.message || 'Não foi possível iniciar o teste.', 'error');
        this.carregando = false;
      }
    });
  }

  iniciarTeste() {
    if (!this.testeAtual) return;
    
    this.estado = EstadoTeste.EM_PROGRESSO;
    this.questaoAtual = 0;
    this.respostas.clear();
    
    // Iniciar timer
    this.tempoRestante = this.testeAtual.tempoLimiteMinutos * 60;
    this.iniciarTimer();
  }

  iniciarTimer() {
    this.atualizarTempoFormatado();
    this.timerInterval = setInterval(() => {
      this.tempoRestante--;
      this.atualizarTempoFormatado();
      
      if (this.tempoRestante <= 0) {
        this.pararTimer();
        Swal.fire({
          title: 'Tempo Esgotado!',
          text: 'O tempo limite foi atingido. O teste será enviado automaticamente.',
          icon: 'warning',
          confirmButtonText: 'OK'
        }).then(() => {
          this.finalizarTeste();
        });
      }
    }, 1000);
  }

  pararTimer() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
      this.timerInterval = null;
    }
  }

  atualizarTempoFormatado() {
    const minutos = Math.floor(this.tempoRestante / 60);
    const segundos = this.tempoRestante % 60;
    this.tempoFormatado = `${minutos.toString().padStart(2, '0')}:${segundos.toString().padStart(2, '0')}`;
  }

  selecionarResposta(questaoId: number, resposta: string) {
    this.respostas.set(questaoId, resposta);
  }

  getRespostaSelecionada(questaoId: number): string {
    return this.respostas.get(questaoId) || '';
  }

  proximaQuestao() {
    if (this.testeAtual && this.questaoAtual < this.testeAtual.questoes.length - 1) {
      this.questaoAtual++;
    }
  }

  questaoAnterior() {
    if (this.questaoAtual > 0) {
      this.questaoAtual--;
    }
  }

  irParaQuestao(index: number) {
    this.questaoAtual = index;
  }

  getQuestaoAtual() {
    if (!this.testeAtual) return null;
    return this.testeAtual.questoes[this.questaoAtual];
  }

  getTotalQuestoes(): number {
    return this.testeAtual?.questoes.length || 0;
  }

  getQuestoesRespondidas(): number {
    return this.respostas.size;
  }

  podeFinalizar(): boolean {
    return this.respostas.size === this.getTotalQuestoes();
  }

  finalizarTeste() {
    if (!this.testeAtual || !this.dataInicio) return;

    // Verificar se todas as questões foram respondidas
    if (this.respostas.size < this.testeAtual.questoes.length) {
      Swal.fire({
        title: 'Atenção!',
        text: `Você ainda não respondeu todas as questões (${this.respostas.size}/${this.testeAtual.questoes.length}). Deseja finalizar mesmo assim?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sim, finalizar',
        cancelButtonText: 'Continuar'
      }).then((result) => {
        if (result.isConfirmed) {
          this.enviarTeste();
        }
      });
    } else {
      this.enviarTeste();
    }
  }

  enviarTeste() {
    if (!this.testeAtual || !this.dataInicio) return;

    this.pararTimer();
    this.carregando = true;

    const respostas: RespostaQuestaoDto[] = Array.from(this.respostas.entries()).map(([questaoId, resposta]) => ({
      questaoId,
      respostaEscolhida: resposta
    }));

    // Preencher questões não respondidas com resposta vazia
    this.testeAtual.questoes.forEach(questao => {
      if (!this.respostas.has(questao.questaoId)) {
        respostas.push({
          questaoId: questao.questaoId,
          respostaEscolhida: ''
        });
      }
    });

    const dto: SubmeterTesteDto = {
      testeId: this.testeAtual.testeId,
      usuarioId: this.usuarioId,
      dataInicio: this.dataInicio.toISOString(),
      respostas: respostas
    };

    this.testeService.submeterTeste(dto).subscribe({
      next: (resultado) => {
        this.resultado = resultado;
        this.estado = EstadoTeste.RESULTADO;
        this.carregando = false;
      },
      error: (error) => {
        console.error('Erro ao submeter teste:', error);
        Swal.fire('Erro', error.error?.message || 'Não foi possível enviar o teste.', 'error');
        this.carregando = false;
      }
    });
  }

  voltarParaLista() {
    this.estado = EstadoTeste.LISTA;
    this.testeAtual = null;
    this.resultado = null;
    this.respostas.clear();
    this.pararTimer();
    this.carregarTestesDisponiveis();
  }

  getDificuldadeNome(dificuldade: string): string {
    const dificuldades: { [key: string]: string } = {
      'Inicial': 'Inicial',
      'Intermediario': 'Intermediário',
      'Avancado': 'Avançado'
    };
    return dificuldades[dificuldade] || dificuldade;
  }

  getNivelNome(nivel: string | null): string {
    if (!nivel) return 'Nenhum';
    const niveis: { [key: string]: string } = {
      'Explorador': 'Explorador',
      'Praticante': 'Praticante',
      'Construtor': 'Construtor',
      'Mestre': 'Mestre'
    };
    return niveis[nivel] || nivel;
  }
}
