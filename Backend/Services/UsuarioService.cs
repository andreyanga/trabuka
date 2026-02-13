using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioReadDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(MapToReadDto);
        }

        public async Task<UsuarioReadDto> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado");

            return MapToReadDto(usuario);
        }

        public async Task<UsuarioReadDto> CreateAsync(UsuarioCreateDto dto)
        {
            // Validações de negócio
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email é obrigatório");

            if (string.IsNullOrWhiteSpace(dto.Senha))
                throw new ArgumentException("Senha é obrigatória");

            // Hash da senha
            var senhaHash = HashPassword(dto.Senha);

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = senhaHash,
                TipoUsuario = dto.TipoUsuario,
                Nivel = null, // Inicia sem nível
                CV = dto.CV ?? "",
                Habilidades = dto.Habilidades ?? "",
                FotoPerfil = dto.FotoPerfil ?? "",
                Telefone = dto.Telefone,
                Status = Models.Enums.StatusUsuario.Pendente
            };

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return MapToReadDto(usuario);
        }

        public async Task<UsuarioReadDto> UpdateAsync(int id, UsuarioUpdateDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado");

            // Atualizar apenas campos preenchidos
            if (!string.IsNullOrWhiteSpace(dto.Nome))
                usuario.Nome = dto.Nome;
            if (!string.IsNullOrWhiteSpace(dto.CV))
                usuario.CV = dto.CV;
            if (!string.IsNullOrWhiteSpace(dto.Habilidades))
                usuario.Habilidades = dto.Habilidades;
            if (!string.IsNullOrWhiteSpace(dto.FotoPerfil))
                usuario.FotoPerfil = dto.FotoPerfil;
            if (!string.IsNullOrWhiteSpace(dto.Telefone))
                usuario.Telefone = dto.Telefone;
            if (dto.Nivel.HasValue)
                usuario.Nivel = dto.Nivel.Value;
            if (dto.Status.HasValue)
                usuario.Status = dto.Status.Value;

            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return MapToReadDto(usuario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return false;

            _usuarioRepository.Delete(usuario);
            return await _usuarioRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsuarioReadDto>> GetByTipoAsync(TrabukaApi.Models.Enums.TipoUsuario tipo)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuariosFiltrados = usuarios.Where(u => u.TipoUsuario == tipo);
            return usuariosFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<UsuarioReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusUsuario status)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var filtrados = usuarios.Where(u => u.Status == status);
            return filtrados.Select(MapToReadDto);
        }

        public async Task<UsuarioReadDto> AtualizarFotoPerfilAsync(int id, string caminhoFoto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;
            usuario.FotoPerfil = caminhoFoto;
            await _usuarioRepository.SaveChangesAsync();
            return MapToReadDto(usuario);
        }

        private static UsuarioReadDto MapToReadDto(Usuario usuario)
        {
            // Corrigir caminho da foto de perfil
            var fotoPerfil = usuario.FotoPerfil;
            if (!string.IsNullOrEmpty(fotoPerfil))
            {
                // Se o caminho ainda contém "src/", remover e usar apenas o nome do arquivo
                if (fotoPerfil.Contains("src/"))
                {
                    fotoPerfil = Path.GetFileName(fotoPerfil);
                }
                // Se não tem extensão, adicionar .png como padrão
                if (!Path.HasExtension(fotoPerfil))
                {
                    fotoPerfil += ".png";
                }
                
                // Adicionar o caminho completo para acesso direto
                fotoPerfil = $"/assets/images/usuarios/{fotoPerfil}";
            }

            return new UsuarioReadDto
            {
                Id = usuario.UsuarioId,
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuario,
                Nivel = usuario.Nivel,
                CV = usuario.CV,
                Habilidades = usuario.Habilidades,
                FotoPerfil = fotoPerfil,
                Telefone = usuario.Telefone,
                Status = usuario.Status
            };
        }

        private static string HashPassword(string senha)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 