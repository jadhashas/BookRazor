using BlazorAppWASM.Models;
using BlazorAppWASM.Services.Interfaces;
using System.Security.Claims;

namespace BlazorAppWASM.Services
{

    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "authToken";
        private Utilisateur? _utilisateurCourant;
        private bool _isInitialized = false;

        public AuthService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        private async Task InitializeAsync()
        {
            if (!_isInitialized)
            {
                await GetUtilisateurCourant();
                _isInitialized = true;
            }
        }

        public async Task<bool> Inscription(Utilisateur utilisateur)
        {
            try
            {
                var utilisateurs = await _localStorage.GetItemAsync<List<Utilisateur>>("utilisateurs") ?? new List<Utilisateur>();

                if (utilisateurs.Any(u => u.Email == utilisateur.Email))
                {
                    return false;
                }

                utilisateurs.Add(utilisateur);
                await _localStorage.SetItemAsync("utilisateurs", utilisateurs);

                return true;
            }
            catch
            {
                return false;
            }
        }





        public async Task<bool> Connexion(string email, string motDePasse)
        {
            try
            {
                var utilisateurs = await _localStorage.GetItemAsync<List<Utilisateur>>("utilisateurs");

                if (utilisateurs == null || !utilisateurs.Any(u => u.Email == email && u.MotDePasse == motDePasse))
                {
                    return false;
                }

                _utilisateurCourant = utilisateurs.First(u => u.Email == email && u.MotDePasse == motDePasse);

                var token = "fake-token-" + _utilisateurCourant.Id;

                await _localStorage.SetItemAsync(StorageKey, token);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task Deconnexion()
        {
            try
            {
                await _localStorage.RemoveItemAsync(StorageKey);
                _utilisateurCourant = null;
                _isInitialized = false;
            }
            catch
            {
                // erreurs
            }
        }

        public async Task<Utilisateur?> GetUtilisateurCourant()
        {
            try
            {
                if (_utilisateurCourant != null)
                    return _utilisateurCourant;

                var token = await _localStorage.GetItemAsync<string>(StorageKey);
                if (string.IsNullOrEmpty(token))
                    return null;

                // Récupérer tous les utilisateurs et trouver celui correspondant au token
                var utilisateurs = await _localStorage.GetItemAsync<List<Utilisateur>>("utilisateurs");
                _utilisateurCourant = utilisateurs?.FirstOrDefault(u => "fake-token-" + u.Id == token);

                return _utilisateurCourant;
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> EstConnecte()
        {
            await InitializeAsync();
            return _utilisateurCourant != null;
        }

        public async Task<bool> EstAdmin()
        {
            await InitializeAsync();
            return _utilisateurCourant?.Role == "Admin";
        }
    }
} 