using BlazorAppWASM.DAL.Data;
using BlazorAppWASM.DAL.Entities;
using BlazorAppWASM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppWASM.Services.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly DbContexte _context;

        public UtilisateurService(DbContexte context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllAsync()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        public async Task<Utilisateur?> GetByIdAsync(int id)
        {
            return await _context.Utilisateurs.FindAsync(id);
        }

        public async Task<Utilisateur> AddAsync(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();
            return utilisateur;
        }

        public async Task<Utilisateur?> UpdateAsync(Utilisateur utilisateur)
        {
            var existing = await _context.Utilisateurs.FindAsync(utilisateur.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(utilisateur);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null) return false;

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
