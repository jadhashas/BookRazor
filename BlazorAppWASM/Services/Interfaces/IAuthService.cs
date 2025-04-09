using BlazorAppWASM.Models;

namespace BlazorAppWASM.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Inscription(Utilisateur utilisateur);
        Task<bool> Connexion(string email, string motDePasse);
        Task Deconnexion();
        Task<Utilisateur?> GetUtilisateurCourant();
        Task<bool> EstConnecte();
        Task<bool> EstAdmin();
    }
}
