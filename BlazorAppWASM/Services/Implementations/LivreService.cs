using System.Net.Http.Json;
using System.IO;
using BlazorAppWASM.Models;
using BlazorAppWASM.Services.Interfaces;
using BlazorAppWASM.Config;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorAppWASM.Services.Implementations
{
    public class LivreService : ILivreService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public LivreService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<IEnumerable<Livre>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Livre>>("api/livre");
                return response ?? Enumerable.Empty<Livre>();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Livre>();
            }
        }

        public async Task<Livre?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Livre>($"api/livre/{id}");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<Livre?> AddAsync(Livre livre)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(livre, new JsonSerializerOptions 
                { 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });
                
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Livre", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Livre>();
                    return result;
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erreur API: {response.StatusCode} - {errorContent}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception dans AddAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Livre?> UpdateAsync(Livre livre)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/livre/{livre.Id}", livre);
                if (response.IsSuccessStatusCode)
                {
                    return livre;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/livre/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UploadImageAsync(int livreId, Stream imageStream, string fileName)
        {
            try
            {
                var content = new MultipartFormDataContent();
                var streamContent = new StreamContent(imageStream);
                content.Add(streamContent, "image", fileName);

                var response = await _httpClient.PostAsync($"api/livre/{livreId}/image", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
} 