using Recrutment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Recrutment.Data
{
    public class RecrutementDbContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Recruteur> Recruteurs { get; set; }
        public DbSet<Candidats> Candidats { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));

        }
        public RecrutementDbContext(DbContextOptions<RecrutementDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations entre les entités (si nécessaire)
            modelBuilder.Entity<Offre>()
               .HasOne(o => o.Recruteur)
               .WithMany(r => r.Offres)
               .HasForeignKey(o => o.IdRecruteur);

            modelBuilder.Entity<Candidature>()
                        .HasOne(c => c.Candidat)
                        .WithMany(c => c.Candidatures)
                        .HasForeignKey(c => c.CandidatId);

            modelBuilder.Entity<Candidature>()
                         .HasOne(c => c.Offre)
                         .WithMany(o => o.Candidatures)
                          .HasForeignKey(c => c.OffreId);

            modelBuilder.Entity<Candidats>().HasData(
            new Candidats
            {
               Id = 1,
               Nom = "Alice",
               Prenom = "Martin",
               Age = 30,
               Titre = "Ingénieur",
               Diplome = "Master en Informatique",
                AnneeExperience = 5,
                Email = "aymaneamg300@email.com",
                CV = "http://example.com/cv/alice.pdf"
            },
            new Candidats
            {
            Id = 2,
            Nom = "Paul",
            Prenom = "Dupuis",
            Age = 28,
            Titre = "Technicien",
            Diplome = "BTS en Réseaux",
            AnneeExperience = 3,
            Email = "aymaneelaamaj123456@email.com",
             CV = "http://example.com/cv/paul.pdf"
            });

            modelBuilder.Entity<Candidature>().HasData(
                new Candidature
                {
                    Id = 1,
                    OffreId = 1, // Offre "Développeur Backend" (ID 1)
                    CandidatId = 1, // Candidat "Alice Martin" (ID 1)
                    DatePostulation = new DateTime(2025, 1, 10)
                },
                new Candidature
                {
                    Id = 2,
                    OffreId = 2, // Offre "Community Manager" (ID 2)
                    CandidatId = 2, // Candidat "Paul Dupuis" (ID 2)
                    DatePostulation = new DateTime(2025, 1, 12)
                });


            // Insérer des données d'exemple dans la base de données au démarrage
            modelBuilder.Entity<Recruteur>().HasData(
                new Recruteur
                {
                    Id = 1,
                    Nom = "Jean Dupont",
                    Tel = "0123456789",
                    Email = "jean.dupont@email.com",
                    MotDePasse = "password123",
                }, new Recruteur
                {
                    Id = 2,
                    Nom = "Aymane Elaamaj",
                    Tel = "0624579",
                    Email = "aymane.elaamaj@email.com",
                    MotDePasse = "aymaneelaamaj",
                }
                );
            

            modelBuilder.Entity<Offre>().HasData(
                new Offre
                {
                    Id = 1,
                    IdRecruteur = 1,
                    TypeContrat = "CDI",
                    Secteur = "Informatique",
                    Profil = "Développeur Full Stack",
                    Poste = "Développeur Backend",
                    Remuneration = 45000
                },
                new Offre
                {
                    Id = 2,
                    IdRecruteur = 1,
                    TypeContrat = "CDD",
                    Secteur = "Marketing",
                    Profil = "Chargé de communication",
                    Poste = "Community Manager",
                    Remuneration = 30000
                });
                    modelBuilder.Entity<ApplicationUser>().HasData(
                    new ApplicationUser
                    {
                        Id = "userId1",
                        Nom = "jean.dupont@email.com",
                        Adresse = "Rue de Paris",
                        CodePostal = "75000",
                      },
                     new ApplicationUser
                     {
                      Id = "userId2",
                      Nom = "aymane.elaamaj@email.com",
                         Adresse = "Rue de Rabat",
                         CodePostal = "10000",
                      }
);


        }
    }
}
