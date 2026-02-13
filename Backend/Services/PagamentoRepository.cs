using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly TrabukaDbContext _context;

        public PagamentoRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pagamento>> GetAllAsync()
        {
            return await _context.Pagamentos.ToListAsync();
        }

        public async Task<Pagamento> GetByIdAsync(int id)
        {
            return await _context.Pagamentos.FindAsync(id);
        }

        public async Task AddAsync(Pagamento pagamento)
        {
            await _context.Pagamentos.AddAsync(pagamento);
        }

        public void Update(Pagamento pagamento)
        {
            _context.Pagamentos.Update(pagamento);
        }

        public void Delete(Pagamento pagamento)
        {
            _context.Pagamentos.Remove(pagamento);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 