namespace Recrutment.Models
{
    public class Candidature
    {
        public int Id { get; set; } // Clé primaire

        // Relation avec Offre
        public int OffreId { get; set; } // Clé étrangère vers Offre
        public Offre Offre { get; set; }

        // Relation avec Candidat
        public int CandidatId { get; set; } // Claé étrangère vers Candidat
        public Candidats Candidat { get; set; }

        public DateTime DatePostulation { get; set; } // Date de la candidature

    }
}
