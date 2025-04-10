using BlazorAppWASM.Models;
using System.IO;

namespace BlazorAppWASM.Services.Interfaces
{
    public interface ILivreService
    {
        Task<IEnumerable<Livre>> GetAllAsync();
        Task<Livre?> GetByIdAsync(int id);
        Task<Livre?> AddAsync(Livre livre);
        Task<Livre?> UpdateAsync(Livre livre);
        Task<bool> DeleteAsync(int id);
        Task<bool> UploadImageAsync(int livreId, Stream imageStream, string fileName);
    }
} 