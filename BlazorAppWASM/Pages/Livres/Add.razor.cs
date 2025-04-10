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