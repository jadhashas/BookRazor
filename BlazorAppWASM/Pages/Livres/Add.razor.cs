using BlazorAppWASM.Models;
using BlazorAppWASM.Services;
using Microsoft.AspNetCore.Components;
using BlazorAppWASM.Components;
using Microsoft.AspNetCore.Components.Forms;
using BlazorAppWASM.Services.Interfaces;

namespace BlazorAppWASM.Pages.Livres;

public partial class AddBase : ComponentBase
{
    [Inject]
    protected ILivreService LivreManager { get; set; } = default!;

    [Inject]
    protected NavigationManager Navigation { get; set; } = default!;

    protected bool _isLoading;
    protected string? _errorMessage;

    protected async Task SaveLivreAsync(LivreForm.SaveLivreEventArgs args)
    {
        try
        {
            _isLoading = true;
            _errorMessage = null;

            // Ne pas envoyer l'URL en base64 à l'API
            if (args.ImageUrl?.StartsWith("data:") == true)
            {
                args.Livre.ImageUrl = null; // On laisse l'API générer l'URL après l'upload
            }
            else if (string.IsNullOrEmpty(args.Livre.ImageUrl))
            {
                // Si aucune image n'est fournie, utiliser une image par défaut
                args.Livre.ImageUrl = $"https://picsum.photos/200/300?random={Random.Shared.Next(1, 1000)}";
            }

            // Sauvegarder le livre
            var livre = await LivreManager.AddAsync(args.Livre);
            
            if (livre == null)
            {
                _errorMessage = "Une erreur est survenue lors de l'ajout du livre.";
                return;
            }

            // Si une image a été uploadée, l'envoyer à l'API
            if (args.Image != null)
            {
                try
                {
                    using var stream = args.Image.OpenReadStream();
                    var success = await LivreManager.UploadImageAsync(livre.Id, stream, args.Image.Name);
                    
                    if (!success)
                    {
                        _errorMessage = "Le livre a été créé mais l'upload de l'image a échoué.";
                        // On continue quand même pour rediriger vers la liste
                    }
                }
                catch (Exception ex)
                {
                    _errorMessage = $"Erreur lors de l'upload de l'image : {ex.Message}";
                    // On continue quand même pour rediriger vers la liste
                }
            }

            if (string.IsNullOrEmpty(_errorMessage))
            {
                Navigation.NavigateTo("/livres");
            }
        }
        catch (Exception ex)
        {
            _errorMessage = $"Erreur lors de l'ajout du livre : {ex.Message}";
        }
        finally
        {
            _isLoading = false;
        }
    }

    protected void CancelAdd()
    {
        Navigation.NavigateTo("/livres");
    }
} 