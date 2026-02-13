using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class FAQRepository : IFAQRepository
    {
        private readonly TrabukaDbContext _context;

        public FAQRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FAQ>> GetAllAsync()
        {
            return await _context.FAQs.ToListAsync();
        }

        public async Task<FAQ> GetByIdAsync(int id)
        {
            return await _context.FAQs.FindAsync(id);
        }

        public async Task AddAsync(FAQ faq)
        {
            await _context.FAQs.AddAsync(faq);
        }

        public void Update(FAQ faq)
        {
            _context.FAQs.Update(faq);
        }

        public void Delete(FAQ faq)
        {
            _context.FAQs.Remove(faq);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 