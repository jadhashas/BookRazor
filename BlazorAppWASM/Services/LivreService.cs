using BlazorAppWASM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAppWASM.Services
{
    public class LivreService
    {
        private List<Livre> _livres = new List<Livre>();
        private int _nextId = 1;

        public async Task<List<Livre>> GetLivresAsync()
        {
            // Simuler un délai pour les opérations asynchrones
            await Task.Delay(100);
            return _livres.ToList();
        }

        public async Task<Livre?> GetLivreByIdAsync(int id)
        {
            await Task.Delay(100);
            return _livres.FirstOrDefault(l => l.Id == id);
        }

        public async Task AddLivreAsync(Livre livre)
        {
            await Task.Delay(100);
            livre.Id = _nextId++;
            _livres.Add(livre);
        }

        public async Task UpdateLivreAsync(Livre livre)
        {
            await Task.Delay(100);
            var existingLivre = _livres.FirstOrDefault(l => l.Id == livre.Id);
            if (existingLivre != null)
            {
                existingLivre.Titre = livre.Titre;
                existingLivre.Auteur = livre.Auteur;
                existingLivre.ISBN = livre.ISBN;
                existingLivre.DatePublication = livre.DatePublication;
                existingLivre.Prix = livre.Prix;
                existingLivre.Description = livre.Description;
            }
        }

        public async Task DeleteLivreAsync(int id)
        {
            await Task.Delay(100);
            var livre = _livres.FirstOrDefault(l => l.Id == id);
            if (livre != null)
            {
                _livres.Remove(livre);
            }
        }

        // Méthodes synchrones pour la compatibilité
        public IEnumerable<Livre> GetAllLivres()
        {
            return _livres;
        }

        public Livre? GetLivreById(int id)
        {
            return _livres.FirstOrDefault(l => l.Id == id);
        }

        public void AddLivre(Livre livre)
        {
            livre.Id = _nextId++;
            _livres.Add(livre);
        }

        public void UpdateLivre(Livre livre)
        {
            var existingLivre = _livres.FirstOrDefault(l => l.Id == livre.Id);
            if (existingLivre != null)
            {
                existingLivre.Titre = livre.Titre;
                existingLivre.Auteur = livre.Auteur;
                existingLivre.ISBN = livre.ISBN;
                existingLivre.DatePublication = livre.DatePublication;
                existingLivre.Prix = livre.Prix;
                existingLivre.Description = livre.Description;
            }
        }

        public void DeleteLivre(int id)
        {
            var livre = _livres.FirstOrDefault(l => l.Id == id);
            if (livre != null)
            {
                _livres.Remove(livre);
            }
        }
    }
} 