using drustvena_mreza.Model;
using drustvena_mreza.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;

namespace drustvena_mreza.Controllers
{
    [Route("api/groups/{groupId}/users")]
    [ApiController]
    public class GrupaKorisnikController : ControllerBase
    {
        KorisnikRepository korisnikRepository = new KorisnikRepository();
        GrupaRepository grupaRepository = new GrupaRepository();

        [HttpGet]
        public ActionResult<List<Korisnik>> GetUsersByGroup(int groupId)
        {
            if (!GrupaRepository.Data.ContainsKey(groupId))
            {
                return NotFound();
            }

            List<Korisnik> korisnici = new List<Korisnik>();
            foreach (Grupa g in GrupaRepository.Data.Values)
            {
                if (g.Id == groupId)
                {
                    korisnici = g.ListaKorisnika;
                }
            }

            return Ok(korisnici);
        }


    }

}
