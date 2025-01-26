using System.ComponentModel.DataAnnotations;

namespace Recrutment.Models
{
    public class Recruteur
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire")]
        [Phone(ErrorMessage = "Numéro de téléphone invalide")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Adresse email invalide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(100, ErrorMessage = "Le mot de passe doit avoir au moins {2} caractères", MinimumLength = 6)]
        public string MotDePasse { get; set; }

        public ICollection<Offre> Offres { get; set; }
        
        
    }
}
