using System.ComponentModel.DataAnnotations;

namespace Recrutment.Models
{
    public class Candidats
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
        public string Titre { get; set; }
        public string Diplome { get; set; }
        public int AnneeExperience { get; set; }
        public string CV { get; set; }
        public string Email { get; set; } // Ajout de la propriété Email
        public ICollection<Candidature> Candidatures { get; set; }
    }
}
