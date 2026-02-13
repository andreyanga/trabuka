using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly TrabukaDbContext _context;

        public PortfolioRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Portfolio>> GetAllAsync()
        {
            return await _context.Portfolios.ToListAsync();
        }

        public async Task<Portfolio> GetByIdAsync(int id)
        {
            return await _context.Portfolios.FindAsync(id);
        }

        public async Task AddAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
        }

        public void Update(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
        }

        public void Delete(Portfolio portfolio)
        {
            _context.Portfolios.Remove(portfolio);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 