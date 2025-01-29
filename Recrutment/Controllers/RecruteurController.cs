using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index()
        {
            // 1. Récupérer l'ID du rôle "Recruteur" depuis la table AspNetRoles
            var roleId = await _context.Roles
                                       .Where(r => r.Name == "Recruteur")
                                       .Select(r => r.Id)
                                       .FirstOrDefaultAsync();

            if (roleId == null)
            {
                return View("Error", new string[] { "Le rôle Recruteur n'existe pas." });
            }

            // 2. Récupérer les IDs des utilisateurs ayant ce rôle dans AspNetUserRoles
            var userIds = await _context.UserRoles
                                        .Where(ur => ur.RoleId == roleId)
                                        .Select(ur => ur.UserId)
                                        .ToListAsync();

            // 3. Récupérer uniquement les utilisateurs ayant cet ID depuis AspNetUsers
            var users = await _context.Users
                                      .Where(u => userIds.Contains(u.Id))
                                      .Select(u => new { u.Id, u.UserName, u.Email })
                                      .ToListAsync();

            // Vérifier s'il y a des utilisateurs avec ce rôle
            if (!users.Any())
            {
                return View("Error", new string[] { "Aucun utilisateur avec le rôle Recruteur trouvé." });
            }

            // 4. Passer les utilisateurs à la vue
            return View(users);
        }

        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
