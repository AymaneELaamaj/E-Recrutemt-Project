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
        private readonly UserManager<ApplicationUser> _userManager; // Utilisateur géré par Identity
        private readonly RoleManager<IdentityRole> _roleManager; // Gestion des rôles

        public RecruteurController(RecrutementDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Affiche la liste des recruteurs
        public async Task<IActionResult> Index()
        {
            // Récupérer le rôle 'Recruteur'
            var role = await _roleManager.FindByNameAsync("Recruteur");

            if (role == null)
            {
                return View("Error", "Le rôle 'Recruteur' n'existe pas.");
            }

            // Récupérer tous les utilisateurs ayant ce rôle
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            // Sélectionner uniquement l'Id et le UserName
            var recruteurs = usersInRole.Select(u => new { u.Id, u.UserName, u.Email }).ToList();

            // Passer les recruteurs à la vue
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
