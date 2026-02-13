using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class FuncionalidadeService : IFuncionalidadeService
    {
        private readonly Data.TrabukaDbContext _context;

        public FuncionalidadeService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FuncionalidadeReadDto>> GetAllAsync()
        {
            var funcionalidades = _context.Funcionalidades.ToList();
            var dtos = funcionalidades.Select(f => MapToReadDto(f)).ToList();
            return await Task.FromResult(dtos);
        }

        public async Task<FuncionalidadeReadDto> GetByIdAsync(int id)
        {
            var funcionalidade = await _context.Funcionalidades.FindAsync(id);
            return funcionalidade == null ? null : MapToReadDto(funcionalidade);
        }

        public async Task<FuncionalidadeReadDto> CreateAsync(FuncionalidadeCreateDto dto)
        {
            var funcionalidade = new Funcionalidade
            {
                PortfolioId = dto.PortfolioId,
                Tipo = dto.Tipo,
                referencia_id = dto.ReferenciaId,
                Titulo = dto.Titulo,
                Icone = dto.Icone,
                Descricao = dto.Descricao,
                created_at = System.DateTime.UtcNow
            };
            _context.Funcionalidades.Add(funcionalidade);
            await _context.SaveChangesAsync();
            return MapToReadDto(funcionalidade);
        }

        public async Task<FuncionalidadeReadDto> UpdateAsync(int id, FuncionalidadeUpdateDto dto)
        {
            var funcionalidade = await _context.Funcionalidades.FindAsync(id);
            if (funcionalidade == null) return null;
            funcionalidade.Tipo = dto.Tipo;
            funcionalidade.referencia_id = dto.ReferenciaId;
            funcionalidade.Titulo = dto.Titulo;
            funcionalidade.Icone = dto.Icone;
            funcionalidade.Descricao = dto.Descricao;
            await _context.SaveChangesAsync();
            return MapToReadDto(funcionalidade);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var funcionalidade = await _context.Funcionalidades.FindAsync(id);
            if (funcionalidade == null) return false;
            _context.Funcionalidades.Remove(funcionalidade);
            await _context.SaveChangesAsync();
            return true;
        }

        private FuncionalidadeReadDto MapToReadDto(Funcionalidade funcionalidade)
        {
            return new FuncionalidadeReadDto
            {
                Id = funcionalidade.id,
                PortfolioId = funcionalidade.PortfolioId,
                Tipo = funcionalidade.Tipo,
                ReferenciaId = funcionalidade.referencia_id,
                Titulo = funcionalidade.Titulo,
                Icone = funcionalidade.Icone,
                Descricao = funcionalidade.Descricao,
                CreatedAt = funcionalidade.created_at
            };
        }
    }
} 