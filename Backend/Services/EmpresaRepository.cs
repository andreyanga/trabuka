using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly TrabukaDbContext _context;

        public EmpresaRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task<Empresa> GetByIdAsync(int id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task AddAsync(Empresa empresa)
        {
            await _context.Empresas.AddAsync(empresa);
        }

        public void Update(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
        }

        public void Delete(Empresa empresa)
        {
            _context.Empresas.Remove(empresa);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 