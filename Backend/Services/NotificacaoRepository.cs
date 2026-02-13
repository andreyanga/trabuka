using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly TrabukaDbContext _context;

        public NotificacaoRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notificacao>> GetAllAsync()
        {
            return await _context.Notificacoes.ToListAsync();
        }

        public async Task<Notificacao> GetByIdAsync(int id)
        {
            return await _context.Notificacoes.FindAsync(id);
        }

        public async Task AddAsync(Notificacao notificacao)
        {
            await _context.Notificacoes.AddAsync(notificacao);
        }

        public void Update(Notificacao notificacao)
        {
            _context.Notificacoes.Update(notificacao);
        }

        public void Delete(Notificacao notificacao)
        {
            _context.Notificacoes.Remove(notificacao);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 