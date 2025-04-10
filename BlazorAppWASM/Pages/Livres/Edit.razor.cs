using BlazorAppWASM.Models;
using BlazorAppWASM.Services;
using Microsoft.AspNetCore.Components;
using BlazorAppWASM.Components;
using Microsoft.AspNetCore.Components.Forms;
using BlazorAppWASM.Services.Interfaces;

namespace BlazorAppWASM.Pages.Livres;

public partial class EditBase : ComponentBase
{
    [Parameter]
    public int LivreId { get; set; }

    [Inject]
    protected ILivreService LivreManager { get; set; } = default!;

    [Inject]
    protected NavigationManager Navigation { get; set; } = default!;

    protected Livre? CurrentLivre;
    protected string? _errorMessage;
    protected bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            CurrentLivre = await LivreManager.GetByIdAsync(LivreId);
            if (CurrentLivre == null)
            {
                Navigation.NavigateTo("/livres");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du chargement du livre : {ex.Message}");
            Navigation.NavigateTo("/livres");
        }
        finally
        {
            _isLoading = false;
        }
    }

    protected async Task SaveLivreAsync(LivreForm.SaveLivreEventArgs args)
    {
        try
        {
            _isLoading = true;
            _errorMessage = null;

            // S'assurer que l'ID est correctement défini
            args.Livre.Id = LivreId;
            
            // Mettre à jour le livre
            var updatedLivre = await LivreManager.UpdateAsync(args.Livre);
            if (updatedLivre == null)
            {
                _errorMessage = "Erreur lors de la mise à jour du livre";
                return;
            }

            // Si une nouvelle image a été uploadée, la sauvegarder
            if (args.Image != null)
            {
                try
                {
                    using var stream = args.Image.OpenReadStream();
                    var success = await LivreManager.UploadImageAsync(LivreId, stream, args.Image.Name);
                    
                    if (!success)
                    {
                        _errorMessage = "Le livre a été mis à jour mais l'upload de l'image a échoué";
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
            _errorMessage = $"Erreur lors de la mise à jour du livre : {ex.Message}";
            Console.WriteLine($"Erreur lors de la mise à jour du livre : {ex.Message}");
        }
        finally
        {
            _isLoading = false;
        }
    }

    protected void CancelEdit()
    {
        Navigation.NavigateTo("/livres");
    }
} 