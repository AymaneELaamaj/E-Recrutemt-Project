using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recrutment.Data;
using Recrutment.Models;
using Recrutment.Service;
using System.Security.Claims;

namespace Recrutment.Controllers
{
    public class OffreController : Controller
    {
        private readonly RecrutementDbContext _context;

        public OffreController(RecrutementDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Recruteur")]
        public IActionResult MesOffres()
        {
            var recruteurId = User.FindFirstValue(ClaimTypes.NameIdentifier); // L'ID du recruteur connecté

            if (!string.IsNullOrEmpty(recruteurId))
            {
                // Récupérer les offres liées au recruteur avec l'ID du recruteur connecté
                var offres = _context.Offres
                    .Where(o => o.ApplicationUserId == recruteurId)
                    .ToList();

                return View(offres); // Affiche les offres du recruteur
            }
            else
            {
                // Gérer l'erreur si l'ID du recruteur est null ou vide
                return BadRequest("L'ID du recruteur est invalide ou introuvable.");
            }
        }







        // Afficher les candidatures pour une offre spécifique
        [Authorize(Roles = "Admin,Recruteur")]

        public IActionResult Candidatures()
        {
            // Récupérer l'ID du recruteur
            var recruteurId = User.FindFirstValue(ClaimTypes.NameIdentifier); // L'ID est une chaîne

            if (!string.IsNullOrEmpty(recruteurId))
            {
                // Récupérer les candidatures liées aux offres du recruteur
                var candidatures = _context.Candidatures
                                           .Where(c => c.Offre.ApplicationUserId == recruteurId)
                                           .Include(c => c.Offre)
                                           .Include(c => c.Candidat)
                                           .ToList();

                return View(candidatures);
            }
            else
            {
                // Gérer l'erreur si l'ID du recruteur est null ou vide
                return BadRequest("L'ID du recruteur est invalide ou introuvable.");
            }
        }



        // Affiche la liste des offres
        public IActionResult Index()
        {
            var offres = _context.Offres
                .Include(o => o.Recruteur) // Inclure l'entité liée "Recruteur"
                .ToList(); // Récupérer toutes les offres

            return View(offres); // Retourner les offres à la vue
        }

        public IActionResult Details(int id)
        {
            var offre = _context.Offres
                .Include(o => o.Recruteur)
                .FirstOrDefault(o => o.Id == id);

            if (offre == null)
            {
                return NotFound(); // Si l'offre n'est pas trouvée
            }

            return View(offre); // Passer l'offre à la vue
        }

        // Affiche le formulaire pour créer une nouvelle offre
        [Authorize(Roles = "Admin,Recruteur")]

        public IActionResult Create()
        {
            var recruteurs = _context.Users // Utilise le DbSet correspondant aux utilisateurs, ici _context.Users
                .Select(u => new { u.Id, u.UserName }) // Sélectionne l'Id et le nom de l'utilisateur (UserName)
                .ToList();

            ViewBag.Recruteurs = new SelectList(recruteurs, "Id", "UserName");

            var model = new Offre
            {
                ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) // Pré-remplir l'ID du recruteur
            };

            ViewData["Title"] = "Créer une offre";

            return View(model);
        }



        [Authorize(Roles ="Admin,Recruteur")]
        // Enregistre une nouvelle offre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Offre model)
        {

            _context.Offres.Add(model);
            _context.SaveChanges();


            ViewBag.Recruteurs = _context.Recruteurs.ToList();
            return RedirectToAction("Index");

        }

        // Affiche le formulaire de modification
        [Authorize(Roles = "Admin,Recruteur")]
        public IActionResult Edit(int id)
        {
            var offre = _context.Offres.FirstOrDefault(o => o.Id == id);

            if (offre == null)
            {
                return NotFound();
            }

            // Check if the current user is authorized to edit this offer
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (offre.ApplicationUserId != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            // Prepare recruiters for dropdown
            ViewBag.Recruteurs = _context.Users
                .Where(u => u.Id == offre.ApplicationUserId || User.IsInRole("Admin"))
                .Select(u => new { Id = u.Id, Nom = u.UserName })
                .ToList();

            ViewData["Title"] = "Modifier une offre";
            return View(offre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recruteur")]
        public IActionResult Edit(Offre offre)
        {
            try
            {
                var existingOffer = _context.Offres.FirstOrDefault(o => o.Id == offre.Id);
                if (existingOffer == null)
                {
                    return NotFound();
                }

                // Check if the current user is authorized to edit this offer
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (existingOffer.ApplicationUserId != currentUserId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                // Update specific fields from the form
                existingOffer.Poste = offre.Poste;
                existingOffer.Secteur = offre.Secteur;
                existingOffer.TypeContrat = offre.TypeContrat;
                existingOffer.Profil = offre.Profil;
                existingOffer.Remuneration = offre.Remuneration;

                // Only update ApplicationUserId if user is Admin
                if (User.IsInRole("Admin"))
                {
                    existingOffer.ApplicationUserId = offre.ApplicationUserId;
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // If there's an error, repopulate recruiters and return to the edit view
                ViewBag.Recruteurs = _context.Users
                    .Where(u => u.Id == offre.ApplicationUserId || User.IsInRole("Admin"))
                    .Select(u => new { Id = u.Id, Nom = u.UserName })
                    .ToList();

                ViewData["Title"] = "Modifier une offre";
                return View(offre);
            }
        }

        // Affiche la confirmation de suppression
        [Authorize(Roles = "Admin,Recruteur")]

        public IActionResult Delete(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }
        [Authorize(Roles = "Admin,Recruteur")]

        // Supprime une offre
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre != null)
            {
                _context.Offres.Remove(offre);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Recruteur")]

        public async Task<IActionResult> Statistiques(string recruteurId)
        {
            var statistiques = await _context.Offres
                .Where(o => o.ApplicationUserId == recruteurId)
                .Select(o => new
                {
                    Offre = o.Poste,
                    NombreCandidatures = o.Candidatures.Count()
                }).ToListAsync();

            return View(statistiques);
        }


    }
}
