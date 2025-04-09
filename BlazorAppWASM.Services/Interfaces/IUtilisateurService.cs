using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAppWASM.DAL.Entities;

namespace BlazorAppWASM.Services.Interfaces
{
    public interface IUtilisateurService
    {
        Task<IEnumerable<Utilisateur>> GetAllAsync();
        Task<Utilisateur?> GetByIdAsync(int id);
        Task<Utilisateur> AddAsync(Utilisateur utilisateur);
        Task<Utilisateur?> UpdateAsync(Utilisateur utilisateur);
        Task<bool> DeleteAsync(int id);
    }
}
