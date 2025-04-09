using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppWASM.DAL.Entities
{
    public class Utilisateur
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        [StringLength(50, ErrorMessage = "Le nom d'utilisateur ne peut pas dépasser 50 caractères")]
        public required string NomUtilisateur { get; set; }

        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir entre 6 et 100 caractères")]
        public required string MotDePasse { get; set; }

        public string Role { get; set; } = "Utilisateur"; // Par défaut
        public DateTime DateInscription { get; set; } = DateTime.Now;
        public bool EstActif { get; set; } = true;
    }
}
