using drustvena_mreza.Model;
using drustvena_mreza.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace drustvena_mreza.Controllers
{
    [Route("api/korisnici")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private KorisnikRepository korisnikRepository = new KorisnikRepository();

        [HttpGet]
        public ActionResult<List<Korisnik>> GetAll()
        {
            List<Korisnik> listaKorisnika = KorisnikRepository.Data.Values.ToList();
            return Ok(listaKorisnika);
        }

        [HttpGet("{id}")]
        public ActionResult<Korisnik> GetById(int id)
        {
            if (!KorisnikRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }

            return Ok(KorisnikRepository.Data[id]);
        }

        [HttpPost]
        public ActionResult<Korisnik> Create([FromBody] Korisnik noviKorisnik)
        {
            if (string.IsNullOrWhiteSpace(noviKorisnik.Ime) ||
                string.IsNullOrWhiteSpace(noviKorisnik.Prezime) ||
                string.IsNullOrWhiteSpace(noviKorisnik.UserName))
            {
                return BadRequest();
            }

            noviKorisnik.Id = DodeliNoviId(KorisnikRepository.Data.Keys.ToList());
            KorisnikRepository.Data[noviKorisnik.Id] = noviKorisnik;
            korisnikRepository.Save();

            return Ok(noviKorisnik);
        }
        [HttpPut("{id}")]
        public ActionResult<Korisnik> Update([FromBody] Korisnik k, int id)
        {
            if (string.IsNullOrWhiteSpace(k.Ime) ||
                string.IsNullOrWhiteSpace(k.Prezime) ||
                string.IsNullOrWhiteSpace(k.UserName))
            {
                return BadRequest();
            }

            if (!KorisnikRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }

            Korisnik korisnik = KorisnikRepository.Data[id];
            korisnik.Ime = k.Ime;
            korisnik.Prezime = k.Prezime;
            korisnik.UserName = k.UserName;
            korisnik.DatumRodjenja = k.DatumRodjenja;

            korisnikRepository.Save();

            return Ok(korisnik);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!KorisnikRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }

            KorisnikRepository.Data.Remove(id);
            korisnikRepository.Save();

            return NoContent();
        }

        private int DodeliNoviId(List<int> identifikatori)
        {
            int maxId = 0;

            foreach (int i in identifikatori)
            {
                if (i > maxId)
                {
                    maxId = i;
                }
            }

            return maxId + 1;
        }
    }
}
