using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class RedeSocialService : IRedeSocialService
    {
        private readonly Data.TrabukaDbContext _context;

        public RedeSocialService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RedeSocialReadDto>> GetAllAsync()
        {
            var redesSociais = _context.RedesSociais.ToList();
            var dtos = redesSociais.Select(r => MapToReadDto(r)).ToList();
            return await Task.FromResult(dtos);
        }

        public async Task<RedeSocialReadDto> GetByIdAsync(int id)
        {
            var redeSocial = await _context.RedesSociais.FindAsync(id);
            return redeSocial == null ? null : MapToReadDto(redeSocial);
        }

        public async Task<RedeSocialReadDto> CreateAsync(RedeSocialCreateDto dto)
        {
            var redeSocial = new RedeSocial
            {
                PortfolioId = dto.PortfolioId,
                Plataforma = dto.Plataforma,
                URL = dto.URL,
                Icone = dto.Icone,
                created_at = System.DateTime.UtcNow
            };
            _context.RedesSociais.Add(redeSocial);
            await _context.SaveChangesAsync();
            return MapToReadDto(redeSocial);
        }

        public async Task<RedeSocialReadDto> UpdateAsync(int id, RedeSocialUpdateDto dto)
        {
            var redeSocial = await _context.RedesSociais.FindAsync(id);
            if (redeSocial == null) return null;
            redeSocial.Plataforma = dto.Plataforma;
            redeSocial.URL = dto.URL;
            redeSocial.Icone = dto.Icone;
            await _context.SaveChangesAsync();
            return MapToReadDto(redeSocial);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var redeSocial = await _context.RedesSociais.FindAsync(id);
            if (redeSocial == null) return false;
            _context.RedesSociais.Remove(redeSocial);
            await _context.SaveChangesAsync();
            return true;
        }

        private RedeSocialReadDto MapToReadDto(RedeSocial rede)
        {
            return new RedeSocialReadDto
            {
                Id = rede.id,
                PortfolioId = rede.PortfolioId,
                Plataforma = rede.Plataforma,
                URL = rede.URL,
                Icone = rede.Icone,
                CreatedAt = rede.created_at
            };
        }
    }
} 