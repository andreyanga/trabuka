using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly TrabukaDbContext _context;

        public RelatorioRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Relatorio>> GetAllAsync()
        {
            return await _context.Relatorios.ToListAsync();
        }

        public async Task<Relatorio> GetByIdAsync(int id)
        {
            return await _context.Relatorios.FindAsync(id);
        }

        public async Task AddAsync(Relatorio relatorio)
        {
            await _context.Relatorios.AddAsync(relatorio);
        }

        public void Update(Relatorio relatorio)
        {
            _context.Relatorios.Update(relatorio);
        }

        public void Delete(Relatorio relatorio)
        {
            _context.Relatorios.Remove(relatorio);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 