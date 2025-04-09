using System.ComponentModel.DataAnnotations;

namespace BlazorAppWASM.Models.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est requis")]
        public string FirstName { get; set; } = string.Empty;
    }
} 