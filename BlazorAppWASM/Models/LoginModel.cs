using System.ComponentModel.DataAnnotations;

namespace BlazorAppWASM.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis")]
        public string MotDePasse { get; set; } = string.Empty;
    }
} 