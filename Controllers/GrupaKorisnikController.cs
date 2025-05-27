using drustvena_mreza.Model;
using drustvena_mreza.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("{userId}")]
        public IActionResult AddUserToGroup(int groupId, int userId)
        {
            if (!GrupaRepository.Data.ContainsKey(groupId))
                return NotFound("Grupa ne postoji.");

            if (!KorisnikRepository.Data.ContainsKey(userId))
                return NotFound("Korisnik ne postoji.");

            var grupa = GrupaRepository.Data[groupId];
            var korisnik = KorisnikRepository.Data[userId];

            if (grupa.ListaKorisnika.Any(k => k.Id == userId))
                return Conflict("Korisnik je već u grupi.");

            grupa.ListaKorisnika.Add(korisnik);
            grupaRepository.Save(); //

            return Ok("Korisnik dodat u grupu.");
        }

        // Uklanjanje korisnika iz grupe pokusaj 999 999 999 999 999
        [HttpDelete("{userId}")]
        public IActionResult RemoveUserFromGroup(int groupId, int userId)
        {
            if (!GrupaRepository.Data.ContainsKey(groupId))
                return NotFound("Grupa ne postoji.");

            var grupa = GrupaRepository.Data[groupId];
            var korisnik = grupa.ListaKorisnika.FirstOrDefault(k => k.Id == userId);

            if (korisnik == null)
                return NotFound("Korisnik nije u grupi.");

            grupa.ListaKorisnika.Remove(korisnik);
            grupaRepository.Save();

            return NoContent();
        }
    }
}
