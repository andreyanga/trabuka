using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class ResultadoTesteRepository : IResultadoTesteRepository
    {
        private readonly TrabukaDbContext _context;

        public ResultadoTesteRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResultadoTeste>> GetAllAsync()
        {
            return await _context.ResultadosTeste.ToListAsync();
        }

        public async Task<ResultadoTeste> GetByIdAsync(int id)
        {
            return await _context.ResultadosTeste.FindAsync(id);
        }

        public async Task AddAsync(ResultadoTeste resultadoTeste)
        {
            await _context.ResultadosTeste.AddAsync(resultadoTeste);
        }

        public void Update(ResultadoTeste resultadoTeste)
        {
            _context.ResultadosTeste.Update(resultadoTeste);
        }

        public void Delete(ResultadoTeste resultadoTeste)
        {
            _context.ResultadosTeste.Remove(resultadoTeste);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 