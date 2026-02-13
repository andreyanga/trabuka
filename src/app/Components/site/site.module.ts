import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule

import { SiteRoutingModule } from './site-routing.module';
import { SiteComponent } from './site.component';

import { HeroComponent } from './hero/hero.component';
import { AboutComponent } from './about/about.component';
import { HowItWorksComponent } from './how-it-works/how-it-works.component';
import { ParaEmpresasComponent } from './para-empresas/para-empresas.component';
import { ParaEstudantesComponent } from './para-estudantes/para-estudantes.component';
import { ServicesComponent } from './services/services.component';
import { InscricaoComponent } from './inscricao/inscricao.component';
import { BeneficiosComponent } from './beneficios/beneficios.component';
import { FaqComponent } from './faq/faq.component';
import { VagasComponent } from './vagas/vagas.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [
    SiteComponent,
    HeroComponent,
    AboutComponent,
    HowItWorksComponent,
    ParaEmpresasComponent,
    ParaEstudantesComponent,
    ServicesComponent,
    InscricaoComponent,
    BeneficiosComponent,
    FaqComponent,
    VagasComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    SiteRoutingModule,
    FormsModule, // Add FormsModule for ngModel
  ]
})
export class SiteModule { }
