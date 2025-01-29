using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrutment.Models;
using Recrutment.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Recrutment.Controllers
{
    
    public class CandidatureController : Controller
    {
        private readonly RecrutementDbContext _context;

        public CandidatureController(RecrutementDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Recruteur")]

        public IActionResult GetCandidaturesByRecruteur()
        {
            var recruteurId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Récupérer les offres créées par ce recruteur
            var offresDuRecruteur = _context.Offres
                .Where(o => o.ApplicationUserId == recruteurId) // Filtrer par l'ID du recruteur
                .ToList();

            // Récupérer les candidatures pour ces offres
            var candidatures = _context.Candidatures
            .Include(c => c.Candidat)  // Inclure les données du candidat
            .Include(c => c.Offre)  // Inclure les données de l'offre
            .Where(c => c.Offre.ApplicationUserId == recruteurId)  // Filtrer par l'ID du recruteur
            .ToList();

            // Retourner la liste des candidatures à la vue
            return View(candidatures);
        }
        [Authorize(Roles = "Admin")]

        // GET: Candidature
        public IActionResult Index()
        {
            var candidatures = _context.Candidatures
                .Include(c => c.Offre)
                .ThenInclude(o => o.Recruteur)
                .OrderByDescending(c => c.DatePostulation)
                .ToList();

            return View(candidatures);
        }

        // GET: Candidature/Details/5
        [Authorize(Roles = "Admin,Recruteur")]

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidature = _context.Candidatures
                .Include(c => c.Offre)
                .ThenInclude(o => o.Recruteur)
                .FirstOrDefault(c => c.Id == id);

            if (candidature == null)
            {
                return NotFound();
            }

            return View(candidature);
        }
    
        // GET: Candidature/Create/5
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offre = _context.Offres
                .Include(o => o.Recruteur)
                .FirstOrDefault(o => o.Id == id);

            if (offre == null)
            {
                Console.WriteLine($"Offre avec l'ID {id} non trouvée.");

                return NotFound();
            }

            var candidature = new Candidature
            {
                OffreId = offre.Id,
                Offre = offre,
                DatePostulation = DateTime.Now
            };

            return View(candidature);
        }

        // POST: Candidature/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OffreId, CandidatId")] Candidature candidature)
        {
            // Test avec un CandidatId valide (vous pouvez modifier cette valeur pour tester)
            if (candidature.CandidatId == 0)
            {
                // Pour le test, on suppose un CandidatId valide. Remplacez cette valeur par un ID qui existe dans votre base de données.
                candidature.CandidatId = 1; // Assurez-vous que cet ID existe dans votre table Candidats.
            }

            // Assurez-vous que l'offre existe dans la base de données
            var offre = _context.Offres.FirstOrDefault(o => o.Id == candidature.OffreId);
            if (offre == null)
            {
                ModelState.AddModelError("", "L'offre spécifiée n'existe pas.");
                return View(candidature);
            }

            // Assurez-vous que le CandidatId existe dans la table Candidats
            var candidat = _context.Candidats.FirstOrDefault(c => c.Id == candidature.CandidatId);
            if (candidat == null)
            {
                ModelState.AddModelError("", "Le candidat spécifié n'existe pas.");
                return View(candidature);
            }

            // Si tout est valide, ajoutez la candidature
            candidature.DatePostulation = DateTime.Now;
            _context.Candidatures.Add(candidature);
            _context.SaveChanges();

            // Rediriger vers la page d'index ou de confirmation
            return RedirectToAction("Index");
        }

            

        // GET: Candidature/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidature = _context.Candidatures
                .Include(c => c.Offre)
                .ThenInclude(o => o.Recruteur)
                .FirstOrDefault(c => c.Id == id);

            if (candidature == null)
            {
                return NotFound();
            }

            return View(candidature);
        }

        // POST: Candidature/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var candidature = _context.Candidatures.Find(id);

            if (candidature == null)
            {
                return NotFound();
            }

            _context.Candidatures.Remove(candidature);
            _context.SaveChanges();

            TempData["Success"] = "Votre candidature a été annulée avec succès.";
            return RedirectToAction(nameof(Index));
        }

    }

}