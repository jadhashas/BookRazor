using System.ComponentModel.DataAnnotations;

namespace BlazorAppWASM.Models
{
    public class Livre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères")]
        public string Titre { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'auteur est requis")]
        [StringLength(50, ErrorMessage = "Le nom de l'auteur ne peut pas dépasser 50 caractères")]
        public string Auteur { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'ISBN est requis")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "L'ISBN doit contenir entre 10 et 13 caractères")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date de publication est requise")]
        public DateTime DatePublication { get; set; } = DateTime.Now;

        [Range(0, 1000, ErrorMessage = "Le prix doit être entre 0 et 1000")]
        public decimal Prix { get; set; }

        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le type de livre est requis")]
        [StringLength(50, ErrorMessage = "Le type ne peut pas dépasser 50 caractères")]
        public string Type { get; set; } = "Roman";

        public bool Disponible { get; set; } = true;

        [StringLength(255)]
        public string? ImageUrl { get; set; }
    }
} 