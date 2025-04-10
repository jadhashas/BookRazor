using BlazorAppWASM.DAL.Data;
using BlazorAppWASM.DAL.Entities;
using BlazorAppWASM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace BlazorAppWASM.Services.Services
{
    public class LivreService : ILivreService
    {
        private readonly DbContexte _context;
        private readonly IHostEnvironment _environment;

        public LivreService(DbContexte context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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

        public async Task<bool> UploadImageAsync(int livreId, Stream imageStream, string fileName)
        {
            try
            {
                var livre = await _context.Livres.FindAsync(livreId);
                if (livre == null) return false;

                // Créer le dossier images s'il n'existe pas
                var uploadsFolder = Path.Combine(_environment.ContentRootPath, "wwwroot", "images", "books");
                Directory.CreateDirectory(uploadsFolder);

                // Générer un nom de fichier unique
                var extension = Path.GetExtension(fileName);
                var uniqueFileName = $"{livreId}_{DateTime.Now.Ticks}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Sauvegarder l'image
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageStream.CopyToAsync(fileStream);
                }

                // Mettre à jour l'URL de l'image dans la base de données
                livre.ImageUrl = $"/images/books/{uniqueFileName}";
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
