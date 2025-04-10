using BlazorAppWASM.Services.Interfaces;
using Microsoft.JSInterop;

namespace BlazorAppWASM.Services.Implementations
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            try
            {
                var value = await _jsRuntime.InvokeAsync<string>("localStorageInterop.getItem", key);
                return string.IsNullOrEmpty(value) ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
            }
            catch
            {
                return default;
            }
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            try
            {
                var serializedValue = System.Text.Json.JsonSerializer.Serialize(value);
                await _jsRuntime.InvokeVoidAsync("localStorageInterop.setItem", key, serializedValue);
            }
            catch
            {
                //l'erreur
            }
        }

        public async Task RemoveItemAsync(string key)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorageInterop.removeItem", key);
            }
            catch
            {
                //l'erreur
            }
        }
    }
} 