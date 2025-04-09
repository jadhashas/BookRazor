using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAppWASM.DAL.Entities;


namespace BlazorAppWASM.Services.Interfaces
{
    public interface ILivreService
    {
        Task<IEnumerable<Livre>> GetAllAsync();
        Task<Livre?> GetByIdAsync(int id);
        Task<Livre> AddAsync(Livre livre);
        Task<Livre?> UpdateAsync(Livre livre);
        Task<bool> DeleteAsync(int id);
    }
}
