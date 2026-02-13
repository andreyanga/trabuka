using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class QuestaoRepository : IQuestaoRepository
    {
        private readonly TrabukaDbContext _context;

        public QuestaoRepository(TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Questao>> GetByTesteIdAsync(int testeId)
        {
            return await _context.Questoes
                .Where(q => q.TesteId == testeId)
                .OrderBy(q => q.Ordem)
                .ToListAsync();
        }

        public async Task<Questao> GetByIdAsync(int id)
        {
            return await _context.Questoes.FindAsync(id);
        }

        public async Task AddAsync(Questao questao)
        {
            await _context.Questoes.AddAsync(questao);
        }

        public void Update(Questao questao)
        {
            _context.Questoes.Update(questao);
        }

        public void Delete(Questao questao)
        {
            _context.Questoes.Remove(questao);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
