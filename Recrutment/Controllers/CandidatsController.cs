using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrutment.Data;
using Recrutment.Models;

namespace Recrutment.Controllers
{
    public class CandidatsController : Controller
    {
        private readonly RecrutementDbContext _context;

        public CandidatsController(RecrutementDbContext context)
        {
            _context = context;
        }

        // GET: Candidats/Create
        [HttpGet]
        public IActionResult Create(int offreId)
        {
            ViewBag.OffreId = offreId; // Passer l'ID de l'offre à la vue
            return View();
        }


        // POST: Candidats/Create
        // POST: Candidats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Prenom,Age,Titre,Diplome,AnneeExperience,CV,Email")] Candidats candidat, int id)
        {
            

            // Vérifier si un candidat avec cet email existe déjà
            var candidatExist = await _context.Candidats
                                              .FirstOrDefaultAsync(c => c.Email == candidat.Email);

            // Si le candidat n'existe pas, on l'ajoute
            if (candidatExist == null)
            {
                _context.Candidats.Add(candidat);
                await _context.SaveChangesAsync(); // Sauvegarder le nouveau candidat
                candidatExist = candidat; // Utiliser le candidat ajouté
            }

            // Créer une candidature en liant le candidat et l'offre
            var candidature = new Candidature
            {
                CandidatId = candidatExist.Id, // ID du candidat existant ou nouvellement ajouté
                OffreId = id,            // ID de l'offre
                DatePostulation = DateTime.Now
            };

            // Ajouter la candidature et sauvegarder
            _context.Candidatures.Add(candidature);
            await _context.SaveChangesAsync();

            // Rediriger vers la liste des offres ou une autre page
            return RedirectToAction("Index", "Offre");
        }
    }




}


