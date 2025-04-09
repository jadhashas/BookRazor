using BlazorAppWASM.Config;
using BlazorAppWASM.Models;
using BlazorAppWASM.Models.Requests;
using BlazorAppWASM.Models.Responses;
using BlazorAppWASM.Services.Interfaces;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorAppWASM.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "authToken";
        private Utilisateur? _utilisateurCourant;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _httpClient.BaseAddress = new Uri(ApiConfig.BaseUrl);
        }

        public async Task<bool> Inscription(Utilisateur utilisateur)
        {
            try
            {
                var request = new RegisterRequest
                {
                    Email = utilisateur.Email,
                    Password = utilisateur.MotDePasse,
                    FirstName = utilisateur.NomUtilisateur
                };

                var response = await _httpClient.PostAsJsonAsync(ApiConfig.Auth.Register, request);
                return response.IsSuccessStatusCode;
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
                var request = new LoginRequest
                {
                    Email = email,
                    Password = motDePasse
                };

                var response = await _httpClient.PostAsJsonAsync(ApiConfig.Auth.Login, request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    if (result?.Token != null)
                    {
                        await _localStorage.SetItemAsync(StorageKey, result.Token);
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Utilisateur?> GetUtilisateurCourant()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>(StorageKey);
                if (string.IsNullOrEmpty(token))
                    return null;

                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(ApiConfig.Auth.Me);
                if (response.IsSuccessStatusCode)
                {
                    _utilisateurCourant = await response.Content.ReadFromJsonAsync<Utilisateur>();
                    return _utilisateurCourant;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task Deconnexion()
        {
            await _localStorage.RemoveItemAsync(StorageKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _utilisateurCourant = null;
        }

        public async Task<bool> EstConnecte()
        {
            var utilisateur = await GetUtilisateurCourant();
            return utilisateur != null;
        }

        public async Task<bool> EstAdmin()
        {
            var utilisateur = await GetUtilisateurCourant();
            return utilisateur?.Role == "Admin";
        }
    }
} 