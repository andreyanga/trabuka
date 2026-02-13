# Configuração de Imagens - Trabuka API

## 📁 Estrutura de Diretórios

```
wwwroot/
└── assets/
    └── images/
        ├── usuarios/          # Fotos de perfil dos usuários
        ├── projetos/          # Imagens de capa dos projetos
        ├── portfolios/        # Imagens dos portfolios
        └── default/           # Imagens padrão
            ├── default-user.png
            ├── default-project.png
            └── default-portfolio.png
```

## 🚀 URLs de Imagens (Acesso Direto)

### 1. Imagem de Usuário
```http
GET /assets/images/usuarios/{nomeArquivo}
```

**Exemplo:**
```http
GET /assets/images/usuarios/5f0386dc-4778-4b9b-a12b-6f557b4e7a77.png
```

### 2. Imagem de Projeto
```http
GET /assets/images/projetos/{nomeArquivo}
```

**Exemplo:**
```http
GET /assets/images/projetos/7c340d49-411c-41aa-bc7f-0113ac0e8958.jpg
```

### 3. Imagem de Portfolio
```http
GET /assets/images/portfolios/{nomeArquivo}
```

**Exemplo:**
```http
GET /assets/images/portfolios/08446568-deed-4a04-8836-521cb00d54da.jpeg
```

### 4. Imagem Padrão
```http
GET /assets/images/default/{nomeArquivo}
```

**Exemplos:**
```http
GET /assets/images/default/default-user.png
GET /assets/images/default/default-project.png
GET /assets/images/default/default-portfolio.png
```

## 🔧 Como Usar no Frontend

### Angular Service (Simplificado)
```typescript
// imagem.service.ts
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImagemService {
  private apiUrl = environment.apiUrl;

  // Agora a API já retorna URLs completas, então podemos usar diretamente
  getImagemUsuario(url: string): string {
    if (!url) {
      return `${this.apiUrl}/assets/images/default/default-user.png`;
    }
    return `${this.apiUrl}${url}`;
  }

  getImagemProjeto(url: string): string {
    if (!url) {
      return `${this.apiUrl}/assets/images/default/default-project.png`;
    }
    return `${this.apiUrl}${url}`;
  }

  getImagemPortfolio(url: string): string {
    if (!url) {
      return `${this.apiUrl}/assets/images/default/default-portfolio.png`;
    }
    return `${this.apiUrl}${url}`;
  }
}
```

### Component Usage
```typescript
// dashboard.component.ts
import { Component } from '@angular/core';
import { ImagemService } from '../services/imagem.service';

@Component({
  selector: 'app-dashboard',
  template: `
    <img [src]="getImagemUsuario(usuario.fotoPerfil)" 
         [alt]="usuario.nome"
         (error)="onImageError($event)">
  `
})
export class DashboardComponent {
  constructor(private imagemService: ImagemService) {}

  getImagemUsuario(nomeArquivo: string): string {
    return this.imagemService.getImagemUsuario(nomeArquivo);
  }

  onImageError(event: any): void {
    // Fallback para imagem padrão
    event.target.src = this.imagemService.getImagemUsuario('');
  }
}
```

### Template HTML (Simplificado)
```html
<!-- Para usuários -->
<img [src]="apiUrl + usuario.fotoPerfil" 
     [alt]="usuario.nome"
     class="profile-img"
     (error)="onImageError($event)">

<!-- Para projetos -->
<img [src]="apiUrl + projeto.imagemCapa" 
     [alt]="projeto.descricao"
     class="project-img"
     (error)="onImageError($event)">

<!-- Para portfolios -->
<img [src]="apiUrl + portfolio.imagem1" 
     [alt]="portfolio.titulo"
     class="portfolio-img"
     (error)="onImageError($event)">
```

## 📝 Formatos Suportados

- **JPEG/JPG** - `image/jpeg`
- **PNG** - `image/png`
- **GIF** - `image/gif`
- **BMP** - `image/bmp`
- **WebP** - `image/webp`

## ⚠️ Tratamento de Erros

### 1. Imagem não encontrada
Se uma imagem não for encontrada, o endpoint retorna:
- **Status**: `404 Not Found`
- **Mensagem**: "Imagem não encontrada"

### 2. Fallback automático
O sistema automaticamente redireciona para imagens padrão quando:
- O nome do arquivo está vazio ou nulo
- A imagem não existe no servidor

### 3. Tratamento no frontend
```typescript
onImageError(event: any): void {
  const img = event.target;
  const currentSrc = img.src;
  
  // Se já não é uma imagem padrão, tenta carregar a padrão
  if (!currentSrc.includes('/default/')) {
    img.src = this.imagemService.getImagemUsuario('');
  }
}
```

## 🔒 Segurança

- Apenas arquivos de imagem são servidos
- Validação de extensões de arquivo
- Logs de tentativas de acesso a arquivos inexistentes
- Caminhos relativos para evitar directory traversal

## 📊 Performance

- Imagens são servidas diretamente pelo servidor
- Cache de navegador habilitado
- Compressão automática (se configurada no servidor)
- Lazy loading recomendado para múltiplas imagens

## 🛠️ Configuração

### 1. Adicionar imagens padrão
Coloque imagens padrão no diretório `src/assets/images/default/`:
- `default-user.png` - Imagem padrão para usuários
- `default-project.png` - Imagem padrão para projetos
- `default-portfolio.png` - Imagem padrão para portfolios

### 2. Configurar CORS (se necessário)
```csharp
// Program.cs
app.UseCors("AllowAngularApp");
```

### 3. Configurar arquivos estáticos
```csharp
// Program.cs
app.UseStaticFiles();
```

## 📱 Exemplo Completo (Simplificado)

```typescript
// user-profile.component.ts
import { Component, Input } from '@angular/core';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-user-profile',
  template: `
    <div class="user-profile">
      <img [src]="imagemPerfil" 
           [alt]="usuario.nome"
           class="profile-image"
           (error)="onImageError($event)">
      <h3>{{ usuario.nome }}</h3>
      <p>{{ usuario.email }}</p>
    </div>
  `,
  styles: [`
    .profile-image {
      width: 100px;
      height: 100px;
      border-radius: 50%;
      object-fit: cover;
    }
  `]
})
export class UserProfileComponent {
  @Input() usuario: any;
  apiUrl = environment.apiUrl;
  
  get imagemPerfil(): string {
    if (!this.usuario.fotoPerfil) {
      return `${this.apiUrl}/assets/images/default/default-user.png`;
    }
    return `${this.apiUrl}${this.usuario.fotoPerfil}`;
  }

  onImageError(event: any): void {
    event.target.src = `${this.apiUrl}/assets/images/default/default-user.png`;
  }
}
```

## 🎯 Benefícios

1. **Centralização**: Todas as imagens são servidas pela API
2. **Segurança**: Controle de acesso e validação
3. **Performance**: Otimização e cache
4. **Flexibilidade**: Fácil mudança de URLs
5. **Fallback**: Imagens padrão automáticas
6. **Manutenibilidade**: Código organizado e reutilizável 