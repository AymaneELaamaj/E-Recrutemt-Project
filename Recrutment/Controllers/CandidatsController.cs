using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrutment.Data;
using Recrutment.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Recrutment.Controllers
{
    public class CandidatsController : Controller
    {
        private readonly RecrutementDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // To manage file uploads

        public CandidatsController(RecrutementDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Candidats/Create
        [HttpGet]
        public IActionResult Create(int offreId)
        {
            ViewBag.OffreId = offreId; // Passer l'ID de l'offre à la vue
            return View();
        }

        // POST: Candidats/Create (avec upload de CV)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom,Prenom,Age,Titre,Diplome,AnneeExperience,Email")] Candidats candidat, int id, IFormFile CVFile)
        {
            // Vérifier si un candidat avec cet email existe déjà
            var candidatExist = await _context.Candidats.FirstOrDefaultAsync(c => c.Email == candidat.Email);

            if (candidatExist == null)
            {
                // Upload du CV s'il est fourni
                if (CVFile != null && CVFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/cvs");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + CVFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await CVFile.CopyToAsync(fileStream);
                    }

                    candidat.CV = "/uploads/cvs/" + uniqueFileName; // Stocker le chemin du fichier dans la base de données
                }

                _context.Candidats.Add(candidat);
                await _context.SaveChangesAsync(); // Sauvegarder le nouveau candidat
                candidatExist = candidat; // Utiliser le candidat ajouté
            }

            // Créer une candidature en liant le candidat et l'offre
            var candidature = new Candidature
            {
                CandidatId = candidatExist.Id,
                OffreId = id,
                DatePostulation = DateTime.Now
            };

            _context.Candidatures.Add(candidature);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Offre");
        }

        // Action pour télécharger un CV
        public IActionResult DownloadCV(int id)
        {
            var candidat = _context.Candidats.Find(id);
            if (candidat == null || string.IsNullOrEmpty(candidat.CV))
            {
                return NotFound("CV introuvable.");
            }

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, candidat.CV.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Le fichier n'existe pas.");
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(candidat.CV);
            return File(fileBytes, "application/pdf", fileName);
        }
    }
}
