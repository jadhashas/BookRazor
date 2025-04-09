using BlazorAppWASM.Models;
using BlazorAppWASM.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAppWASM.Pages.Livres
{
    public partial class Edit
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private LivreService LivreService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private Livre livre;

        protected override void OnInitialized()
        {
            livre = LivreService.GetLivreById(Id);
            if (livre == null)
            {
                NavigationManager.NavigateTo("/livres");
            }
        }

        private void HandleSave(Livre livre)
        {
            LivreService.UpdateLivre(livre);
            NavigationManager.NavigateTo("/livres");
        }

        private void HandleCancel()
        {
            NavigationManager.NavigateTo("/livres");
        }
    }
} 