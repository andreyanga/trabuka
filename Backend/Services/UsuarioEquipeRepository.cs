using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class UsuarioEquipeRepository : IUsuarioEquipeRepository
    {
        private readonly TrabukaDbContext _context;

        public UsuarioEquipeRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioEquipe>> GetAllAsync()
        {
            return await _context.UsuarioEquipes.ToListAsync();
        }

        public async Task<UsuarioEquipe> GetByIdsAsync(int usuarioId, int equipeId)
        {
            return await _context.UsuarioEquipes
                .FirstOrDefaultAsync(ue => ue.usuario_id == usuarioId && ue.equipe_id == equipeId);
        }

        public async Task AddAsync(UsuarioEquipe usuarioEquipe)
        {
            await _context.UsuarioEquipes.AddAsync(usuarioEquipe);
        }

        public void Update(UsuarioEquipe usuarioEquipe)
        {
            _context.UsuarioEquipes.Update(usuarioEquipe);
        }

        public void Delete(UsuarioEquipe usuarioEquipe)
        {
            _context.UsuarioEquipes.Remove(usuarioEquipe);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 