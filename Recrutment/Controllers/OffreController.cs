using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrutment.Data;
using Recrutment.Models;

namespace Recrutment.Controllers
{
    public class OffreController : Controller
    {
        private readonly RecrutementDbContext _context;

        public OffreController(RecrutementDbContext context)
        {
            _context = context;
        }

        // Affiche la liste des offres
        public IActionResult Index()
        {
            var offres = _context.Offres
                .Include(o => o.Recruteur) // Inclure les recruteurs associés
                .Include(o => o.Candidatures) // Inclure les candidatures associées
                .ToList();

            return View(offres);
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
        public IActionResult Create()
        {
            ViewBag.Recruteurs = _context.Recruteurs.ToList(); // Charger les recruteurs pour le menu déroulant
            return View(new Offre());
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
        public IActionResult Edit(int id)
        {
            var offre = _context.Offres
                .Include(o => o.Recruteur)
                .FirstOrDefault(o => o.Id == id);

            if (offre == null)
            {
                return NotFound();
            }

            ViewBag.Recruteurs = _context.Recruteurs.ToList();
            return View(offre);
        }

        // Enregistre les modifications
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Offre model)
        {
            
            _context.Offres.Update(model);
            _context.SaveChanges();
            

            ViewBag.Recruteurs = _context.Recruteurs.ToList();
            return RedirectToAction("Index");

        }

        // Affiche la confirmation de suppression
        public IActionResult Delete(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }

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
        public async Task<IActionResult> Candidatures(int offreId)
        {
            var offre = await _context.Offres
                .Include(o => o.Candidatures)
                .ThenInclude(c => c.Candidat)
                .FirstOrDefaultAsync(o => o.Id == offreId);

            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }
        public async Task<IActionResult> Statistiques(int recruteurId)
        {
            var statistiques = await _context.Offres
                .Where(o => o.IdRecruteur == recruteurId)
                .Select(o => new
                {
                    Offre = o.Poste,
                    NombreCandidatures = o.Candidatures.Count()
                }).ToListAsync();

            return View(statistiques);
        }


    }
}
