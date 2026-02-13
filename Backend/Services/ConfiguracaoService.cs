using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class ConfiguracaoService : IConfiguracaoService
    {
        private readonly Data.TrabukaDbContext _context;

        public ConfiguracaoService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConfiguracaoReadDto>> GetAllAsync()
        {
            var configs = _context.Configuracoes;
            var dtos = new List<ConfiguracaoReadDto>();
            foreach (var config in configs)
            {
                dtos.Add(MapToReadDto(config));
            }
            return await Task.FromResult(dtos);
        }

        public async Task<ConfiguracaoReadDto> GetByIdAsync(int id)
        {
            var config = await _context.Configuracoes.FindAsync(id);
            return config == null ? null : MapToReadDto(config);
        }

        public async Task<ConfiguracaoReadDto> CreateAsync(ConfiguracaoCreateDto dto)
        {
            var config = new Configuracao
            {
                usuario_id = dto.UsuarioId,
                chave = dto.Chave,
                valor = dto.Valor,
                descricao = dto.Descricao
            };
            _context.Configuracoes.Add(config);
            await _context.SaveChangesAsync();
            return MapToReadDto(config);
        }

        public async Task<ConfiguracaoReadDto> UpdateAsync(int id, ConfiguracaoUpdateDto dto)
        {
            var config = await _context.Configuracoes.FindAsync(id);
            if (config == null) return null;
            config.valor = dto.Valor;
            config.descricao = dto.Descricao;
            await _context.SaveChangesAsync();
            return MapToReadDto(config);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var config = await _context.Configuracoes.FindAsync(id);
            if (config == null) return false;
            _context.Configuracoes.Remove(config);
            await _context.SaveChangesAsync();
            return true;
        }

        private ConfiguracaoReadDto MapToReadDto(Configuracao config)
        {
            return new ConfiguracaoReadDto
            {
                Id = config.id,
                UsuarioId = config.usuario_id,
                Chave = config.chave,
                Valor = config.valor,
                Descricao = config.descricao
            };
        }
    }
} 