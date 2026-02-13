# Migração de Imagens: src → wwwroot

## 📋 Resumo das Mudanças

### ✅ O que foi feito:

1. **Movimentação de arquivos**: Todos os arquivos da pasta `src/assets/images/` foram movidos para `wwwroot/assets/images/`

2. **Atualização de configurações**:
   - `Program.cs`: Adicionado `app.UseStaticFiles()` para servir arquivos estáticos
   - `ImagensController.cs`: Atualizado para usar `_environment.WebRootPath`
   - `FileUploadHelper.cs`: Atualizado para salvar em `wwwroot/assets/images/`

3. **Correção de serviços**:
   - `UsuarioService.cs`: Corrige caminhos de fotos de perfil
   - `ProjetoService.cs`: Corrige caminhos de imagens de capa
   - `PortfolioService.cs`: Corrige caminhos de imagens de portfolio
   - `DashboardService.cs`: Retorna URLs corretas para imagens

4. **Atualização de dados**:
   - `DatabaseUpdater.cs`: Remove caminhos antigos "src/" do banco de dados
   - Executado automaticamente na inicialização da aplicação

### 🔄 URLs Antigas vs Novas:

| Tipo | Antiga | Nova |
|------|--------|------|
| Usuário | `src/assets/images/usuarios/arquivo.png` | `arquivo.png` |
| Projeto | `src/assets/images/projetos/arquivo.jpg` | `arquivo.jpg` |
| Portfolio | `src/assets/images/portfolios/arquivo.jpeg` | `arquivo.jpeg` |

### 🌐 URLs de Acesso:

| Tipo | URL de Acesso |
|------|---------------|
| Usuário | `http://localhost:5006/assets/images/usuarios/arquivo.png` |
| Projeto | `http://localhost:5006/assets/images/projetos/arquivo.jpg` |
| Portfolio | `http://localhost:5006/assets/images/portfolios/arquivo.jpeg` |
| Padrão | `http://localhost:5006/assets/images/default/default-user.png` |

### 📁 Nova Estrutura de Diretórios:

```
wwwroot/
└── assets/
    └── images/
        ├── usuarios/          # Fotos de perfil
        ├── projetos/          # Imagens de capa
        ├── portfolios/        # Imagens de portfolio
        └── default/           # Imagens padrão
            ├── default-user.png
            ├── default-project.png
            └── default-portfolio.png
```

### 🛠️ Como usar no Frontend:

```typescript
// Antes
const imagemUrl = `${apiUrl}/api/imagens/usuarios/${nomeArquivo}`;

// Agora
const imagemUrl = `${apiUrl}/assets/images/usuarios/${nomeArquivo}`;
```

### 🔧 Configuração no Angular:

```typescript
// imagem.service.ts
export class ImagemService {
  private apiUrl = environment.apiUrl;

  getImagemUsuario(nomeArquivo: string): string {
    if (!nomeArquivo) {
      return `${this.apiUrl}/assets/images/default/default-user.png`;
    }
    return `${this.apiUrl}/assets/images/usuarios/${nomeArquivo}`;
  }
}
```

### ✅ Benefícios:

1. **Performance**: Acesso direto aos arquivos estáticos (sem passar pela API)
2. **Simplicidade**: URLs mais limpas e diretas
3. **Padrão**: Segue as convenções do ASP.NET Core
4. **Cache**: Melhor cache do navegador
5. **Escalabilidade**: Fácil de configurar CDN no futuro

### ⚠️ Observações:

- As imagens agora são servidas diretamente pelo servidor web
- Não é mais necessário o `ImagensController` para servir imagens
- O `ImagensController` pode ser removido se não for usado para outras funcionalidades
- Os caminhos no banco de dados são automaticamente corrigidos na inicialização

### 🧪 Teste:

Para verificar se tudo está funcionando:

1. Acesse: `http://localhost:5006/assets/images/usuarios/5f0386dc-4778-4b9b-a12b-6f557b4e7a77.png`
2. A imagem deve carregar diretamente
3. Verifique os endpoints da API para confirmar que retornam apenas o nome do arquivo

### 🗑️ Limpeza:

- A pasta `src/` foi removida
- O `ImagensController` pode ser removido se não for necessário
- Os dados no banco são automaticamente corrigidos 