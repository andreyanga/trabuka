using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class MentoriaRepository : IMentoriaRepository
    {
        private readonly TrabukaDbContext _context;

        public MentoriaRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mentoria>> GetAllAsync()
        {
            return await _context.Mentorias.ToListAsync();
        }

        public async Task<Mentoria> GetByIdAsync(int id)
        {
            return await _context.Mentorias.FindAsync(id);
        }

        public async Task AddAsync(Mentoria mentoria)
        {
            await _context.Mentorias.AddAsync(mentoria);
        }

        public void Update(Mentoria mentoria)
        {
            _context.Mentorias.Update(mentoria);
        }

        public void Delete(Mentoria mentoria)
        {
            _context.Mentorias.Remove(mentoria);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 