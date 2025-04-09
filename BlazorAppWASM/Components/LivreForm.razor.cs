using BlazorAppWASM.Models;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppWASM.Components
{
    public partial class LivreForm
    {
        [Parameter]
        public Livre? Livre { get; set; }

        [Parameter]
        public EventCallback<Livre> OnSave { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private Livre livre = new Livre 
        { 
            DatePublication = DateTime.Today,
            Titre = string.Empty,
            Auteur = string.Empty,
            ISBN = string.Empty
        };

        protected override void OnInitialized()
        {
            if (Livre != null)
            {
                livre = new Livre
                {
                    Id = Livre.Id,
                    Titre = Livre.Titre,
                    Auteur = Livre.Auteur,
                    ISBN = Livre.ISBN,
                    DatePublication = Livre.DatePublication,
                    Prix = Livre.Prix,
                    Description = Livre.Description
                };
            }
        }

        private void HandleValidSubmit()
        {
            OnSave.InvokeAsync(livre);
        }

        private void HandleCancel()
        {
            OnCancel.InvokeAsync();
        }
    }
} 