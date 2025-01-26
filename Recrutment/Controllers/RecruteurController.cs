using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrutment.Data;
using Recrutment.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Recrutment.Service;

namespace Recrutment.Controllers
{
    public class RecruteurController : Controller
    {
        private readonly RecrutementDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOffreService _offreService;

        public RecruteurController(RecrutementDbContext context, UserManager<ApplicationUser> userManager, IOffreService offreService)
        {
            _context = context;
            _userManager = userManager;
            _offreService = offreService;
        }

        // Affiche la liste des recruteurs
        public async Task<IActionResult> Index()
        {
            var recruteurs = await _context.Recruteurs.Include(r => r.Offres).ToListAsync();
            return View(recruteurs);
        }

        // Affiche le formulaire pour créer un nouveau recruteur
        public IActionResult Create()
        {
            return View(new Recruteur());
        }

        // Enregistre un nouveau recruteur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recruteur model)
        {
            if (ModelState.IsValid)
            {
                _context.Recruteurs.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Affiche le formulaire pour modifier un recruteur
        public async Task<IActionResult> Edit(int id)
        {
            var recruteur = await _context.Recruteurs.FindAsync(id);
            if (recruteur == null)
            {
                return NotFound();
            }
            return View(recruteur);
        }

        // Enregistre les modifications d’un recruteur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Recruteur model)
        {
            if (ModelState.IsValid)
            {
                _context.Recruteurs.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Supprime un recruteur
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recruteur = await _context.Recruteurs.FindAsync(id);
            if (recruteur != null)
            {
                _context.Recruteurs.Remove(recruteur);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Récupérer les offres du recruteur connecté
        public async Task<IActionResult> MesOffres()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var recruteurId = user.Id; // ID du recruteur connecté
            var offres = await _offreService.GetOffresByRecruteurId(recruteurId);

            return View(offres);
        }

        // Modifier une offre
        //public async Task<IActionResult> EditOffre(int id)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //        return RedirectToAction("Login", "Account");

        //    var recruteurId = user.Id;
        //    var offre = await _offreService.GetOffreById(id);

        //    if (offre == null || offre.Recruteur != recruteurId)
        //    {
        //        return Unauthorized(); // Accès refusé
        //    }

        //    return View(offre);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditOffre(Offre model)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //        return RedirectToAction("Login", "Account");

        //    var recruteurId = user.Id;

        //    // Vérification de la propriété
        //    if (model.IdRecruteur != recruteurId)
        //    {
        //        return Unauthorized();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _context.Offres.Update(model);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("MesOffres");
        //    }

        //    return View(model);
        //}
    }
}
