using TrabukaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TrabukaApi.Data
{
    public static class DatabaseUpdater
    {
        public static void UpdateImagePaths(TrabukaDbContext context)
        {
            // Atualizar caminhos das fotos de perfil dos usuários
            var usuarios = context.Usuarios.ToList();
            foreach (var usuario in usuarios)
            {
                if (!string.IsNullOrEmpty(usuario.FotoPerfil) && usuario.FotoPerfil.Contains("src/"))
                {
                    usuario.FotoPerfil = Path.GetFileName(usuario.FotoPerfil);
                }
            }

            // Atualizar caminhos das imagens de capa dos projetos
            var projetos = context.Projetos.ToList();
            foreach (var projeto in projetos)
            {
                if (!string.IsNullOrEmpty(projeto.ImagemCapa) && projeto.ImagemCapa.Contains("src/"))
                {
                    projeto.ImagemCapa = Path.GetFileName(projeto.ImagemCapa);
                }
            }

            // Atualizar caminhos das imagens dos portfolios
            var portfolios = context.Portfolios.ToList();
            foreach (var portfolio in portfolios)
            {
                if (!string.IsNullOrEmpty(portfolio.Imagem1) && portfolio.Imagem1.Contains("src/"))
                {
                    portfolio.Imagem1 = Path.GetFileName(portfolio.Imagem1);
                }
                if (!string.IsNullOrEmpty(portfolio.Imagem2) && portfolio.Imagem2.Contains("src/"))
                {
                    portfolio.Imagem2 = Path.GetFileName(portfolio.Imagem2);
                }
            }

            // Salvar as alterações
            context.SaveChanges();
        }
    }
} 