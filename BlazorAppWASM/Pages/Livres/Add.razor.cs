using BlazorAppWASM.Models;
using BlazorAppWASM.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAppWASM.Pages.Livres
{
    public partial class Add
    {
        [Inject]
        private LivreService LivreService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private void HandleSave(Livre livre)
        {
            LivreService.AddLivre(livre);
            NavigationManager.NavigateTo("/livres");
        }

        private void HandleCancel()
        {
            NavigationManager.NavigateTo("/livres");
        }
    }
} 