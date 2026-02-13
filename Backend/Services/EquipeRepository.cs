using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class EquipeRepository : IEquipeRepository
    {
        private readonly TrabukaDbContext _context;

        public EquipeRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Equipe>> GetAllAsync()
        {
            return await _context.Equipes.ToListAsync();
        }

        public async Task<Equipe> GetByIdAsync(int id)
        {
            return await _context.Equipes.FindAsync(id);
        }

        public async Task AddAsync(Equipe equipe)
        {
            await _context.Equipes.AddAsync(equipe);
        }

        public void Update(Equipe equipe)
        {
            _context.Equipes.Update(equipe);
        }

        public void Delete(Equipe equipe)
        {
            _context.Equipes.Remove(equipe);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 