using BlazorAppWASM;
using BlazorAppWASM.Services.Implementations;
using BlazorAppWASM.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5023/") });
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<StateService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ILivreService, LivreService>();

await builder.Build().RunAsync();
