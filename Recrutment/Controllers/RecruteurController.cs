using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recrutment.Data;
using Recrutment.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutment.Controllers
{
    public class RecruteurController : Controller
    {
        private readonly RecrutementDbContext _context;
        

        public RecruteurController(RecrutementDbContext context)
        {
            _context = context;
          
        }

        // Affiche la liste des recruteurs
        public async Task<IActionResult> Index()
        {
            // Récupérer tous les utilisateurs
            var allUsers = await _context.Users.ToListAsync();

            // Vérifier si des utilisateurs existent
            if (allUsers == null || !allUsers.Any())
            {
                return View("Error", new string[] { "Aucun utilisateur trouvé." });
            }

            // Sélectionner uniquement l'Id, UserName et Email
            var users = allUsers.Select(u => new { u.Id, u.UserName, u.Email }).ToList();

            // Passer les utilisateurs à la vue
            return View(users);
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
            
                _context.Recruteurs.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            
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
            
                _context.Recruteurs.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            
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
    }
}
