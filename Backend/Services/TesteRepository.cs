using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class TesteRepository : ITesteRepository
    {
        private readonly TrabukaDbContext _context;

        public TesteRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teste>> GetAllAsync()
        {
            return await _context.Testes.ToListAsync();
        }

        public async Task<Teste> GetByIdAsync(int id)
        {
            return await _context.Testes.FindAsync(id);
        }

        public async Task AddAsync(Teste teste)
        {
            await _context.Testes.AddAsync(teste);
        }

        public void Update(Teste teste)
        {
            _context.Testes.Update(teste);
        }

        public void Delete(Teste teste)
        {
            _context.Testes.Remove(teste);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 