using BlazorAppWASM.DAL.Entities;

namespace BlazorAppWASM.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Inscription(Utilisateur utilisateur);
        Task<(bool success, string token)> Connexion(string email, string motDePasse);
        Task<Utilisateur> GetCurrentUser(string token);
    }
} 