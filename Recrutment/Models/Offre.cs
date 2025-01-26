namespace Recrutment.Models
{
    public class Offre
    {
        public int Id { get; set; }
        public int IdRecruteur { get; set; }
        public string TypeContrat { get; set; }
        public string Secteur { get; set; }
        public string Profil { get; set; }
        public string Poste { get; set; }
        public int Remuneration { get; set; }

        // Relation avec Recruteur
        public Recruteur Recruteur { get; set; }
        public ICollection<Candidature> Candidatures { get; set; }

    }
}
