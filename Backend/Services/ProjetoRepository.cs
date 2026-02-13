using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly TrabukaDbContext _context;

        public ProjetoRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Projeto>> GetAllAsync()
        {
            return await _context.Projetos.ToListAsync();
        }

        public async Task<Projeto> GetByIdAsync(int id)
        {
            return await _context.Projetos.FindAsync(id);
        }

        public async Task AddAsync(Projeto projeto)
        {
            await _context.Projetos.AddAsync(projeto);
        }

        public void Update(Projeto projeto)
        {
            _context.Projetos.Update(projeto);
        }

        public void Delete(Projeto projeto)
        {
            _context.Projetos.Remove(projeto);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 