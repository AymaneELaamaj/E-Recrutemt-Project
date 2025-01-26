﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recrutment.Data;

#nullable disable

namespace Recrutment.Migrations
{
    [DbContext(typeof(RecrutementDbContext))]
    [Migration("20250119152743_FIrsteCreate")]
    partial class FIrsteCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Recrutment.Models.Candidats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("AnneeExperience")
                        .HasColumnType("int");

                    b.Property<string>("CV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diplome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Candidats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 30,
                            AnneeExperience = 5,
                            CV = "http://example.com/cv/alice.pdf",
                            Diplome = "Master en Informatique",
                            Email = "aymaneamg300@email.com",
                            Nom = "Alice",
                            Password = "Aymaneelaamaj123",
                            Prenom = "Martin",
                            Titre = "Ingénieur"
                        },
                        new
                        {
                            Id = 2,
                            Age = 28,
                            AnneeExperience = 3,
                            CV = "http://example.com/cv/paul.pdf",
                            Diplome = "BTS en Réseaux",
                            Email = "aymaneelaamaj123456@email.com",
                            Nom = "Paul",
                            Password = "Aymaneelaamaj12334",
                            Prenom = "Dupuis",
                            Titre = "Technicien"
                        });
                });

            modelBuilder.Entity("Recrutment.Models.Candidature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandidatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePostulation")
                        .HasColumnType("datetime2");

                    b.Property<int>("OffreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CandidatId");

                    b.HasIndex("OffreId");

                    b.ToTable("Candidatures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CandidatId = 1,
                            DatePostulation = new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OffreId = 1
                        },
                        new
                        {
                            Id = 2,
                            CandidatId = 2,
                            DatePostulation = new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OffreId = 2
                        });
                });

            modelBuilder.Entity("Recrutment.Models.Offre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdRecruteur")
                        .HasColumnType("int");

                    b.Property<string>("Poste")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Remuneration")
                        .HasColumnType("int");

                    b.Property<string>("Secteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeContrat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdRecruteur");

                    b.ToTable("Offres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IdRecruteur = 1,
                            Poste = "Développeur Backend",
                            Profil = "Développeur Full Stack",
                            Remuneration = 45000,
                            Secteur = "Informatique",
                            TypeContrat = "CDI"
                        },
                        new
                        {
                            Id = 2,
                            IdRecruteur = 1,
                            Poste = "Community Manager",
                            Profil = "Chargé de communication",
                            Remuneration = 30000,
                            Secteur = "Marketing",
                            TypeContrat = "CDD"
                        });
                });

            modelBuilder.Entity("Recrutment.Models.Recruteur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recruteurs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "jean.dupont@email.com",
                            MotDePasse = "password123",
                            Nom = "Jean Dupont",
                            Tel = "0123456789"
                        },
                        new
                        {
                            Id = 2,
                            Email = "aymane.elaamaj@email.com",
                            MotDePasse = "aymaneelaamaj",
                            Nom = "Aymane Elaamaj",
                            Tel = "0624579"
                        });
                });

            modelBuilder.Entity("Recrutment.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.HasData(
                        new
                        {
                            Id = "userId1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "36796170-d6e3-4b8f-b120-21197cb8bcd1",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0746ccc9-50fb-422e-b1bd-0d2a549f423f",
                            TwoFactorEnabled = false,
                            Adresse = "Rue de Paris",
                            CodePostal = "75000",
                            Nom = "jean.dupont@email.com"
                        },
                        new
                        {
                            Id = "userId2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0da9896c-f8b3-4e46-b890-c24e4c4be6f8",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "430962d3-2738-4943-a425-07db281ac0f8",
                            TwoFactorEnabled = false,
                            Adresse = "Rue de Rabat",
                            CodePostal = "10000",
                            Nom = "aymane.elaamaj@email.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Recrutment.Models.Candidature", b =>
                {
                    b.HasOne("Recrutment.Models.Candidats", "Candidat")
                        .WithMany("Candidatures")
                        .HasForeignKey("CandidatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recrutment.Models.Offre", "Offre")
                        .WithMany("Candidatures")
                        .HasForeignKey("OffreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidat");

                    b.Navigation("Offre");
                });

            modelBuilder.Entity("Recrutment.Models.Offre", b =>
                {
                    b.HasOne("Recrutment.Models.Recruteur", "Recruteur")
                        .WithMany("Offres")
                        .HasForeignKey("IdRecruteur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recruteur");
                });

            modelBuilder.Entity("Recrutment.Models.Candidats", b =>
                {
                    b.Navigation("Candidatures");
                });

            modelBuilder.Entity("Recrutment.Models.Offre", b =>
                {
                    b.Navigation("Candidatures");
                });

            modelBuilder.Entity("Recrutment.Models.Recruteur", b =>
                {
                    b.Navigation("Offres");
                });
#pragma warning restore 612, 618
        }
    }
}