using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutment.Models;
namespace Recrutment.Service
{
    

    public interface IOffreService
    {
        Task<List<Offre>> GetOffresByRecruteurId(string recruteurId);
        Task<Offre> GetOffreById(int id);
    }

}
