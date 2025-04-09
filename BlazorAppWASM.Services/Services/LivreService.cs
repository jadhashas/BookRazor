using BlazorAppWASM.DAL.Data;
using BlazorAppWASM.DAL.Entities;
using BlazorAppWASM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppWASM.Services.Services
{
    public class LivreService : ILivreService
    {
        private readonly DbContexte _context;

        public LivreService(DbContexte context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livre>> GetAllAsync()
        {
            return await _context.Livres.ToListAsync();
        }

        public async Task<Livre?> GetByIdAsync(int id)
        {
            return await _context.Livres.FindAsync(id);
        }

        public async Task<Livre> AddAsync(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();
            return livre;
        }

        public async Task<Livre?> UpdateAsync(Livre livre)
        {
            var existing = await _context.Livres.FindAsync(livre.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(livre);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null) return false;

            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
