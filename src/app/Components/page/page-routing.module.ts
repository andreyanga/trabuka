import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageComponent } from './page.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { VagasComponent } from './vagas/vagas.component';
import { MeusProjetosComponent } from './meus-projetos/meus-projetos.component';
import { MeuPerfilComponent } from './meu-perfil/meu-perfil.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { TesteHabilidadesComponent } from './teste-habilidades/teste-habilidades.component';
import { GanhosComponent } from './ganhos/ganhos.component';
import { ForumComponent } from './forum/forum.component';
import { ConfiguracoesComponent } from './configuracoes/configuracoes.component';
import { AuthGuard } from '../../services/auth.guard';

const routes: Routes = [{
    path: '',
    component: PageComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'vagas', component: VagasComponent },
      { path: 'meus-projetos', component: MeusProjetosComponent },
      { path: 'meu-perfil', component: MeuPerfilComponent },
      { path: 'portfolio', component: PortfolioComponent },
      { path: 'testes-habilidades', component: TesteHabilidadesComponent },
      { path: 'ganhos', component: GanhosComponent },
      { path: 'forum', component: ForumComponent },
      { path: 'configuracoes', component: ConfiguracoesComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PageRoutingModule { }





