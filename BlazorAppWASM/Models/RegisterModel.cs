using System.ComponentModel.DataAnnotations;

namespace BlazorAppWASM.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        public string NomUtilisateur { get; set; }

        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        public string MotDePasse { get; set; }

        [Required(ErrorMessage = "La confirmation du mot de passe est requise")]
        [Compare("MotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmMotDePasse { get; set; }
    }
} 