using drustvena_mreza.Model;
using drustvena_mreza.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace drustvena_mreza.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GrupaController : ControllerBase
    {
        private readonly GrupaRepository grupaRepository = new GrupaRepository();
        [HttpGet]
        public ActionResult<List<Grupa>> GetAll()
        {
            return Ok(GrupaRepository.Data.Values.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Grupa> GetById(int id)
        {
            if (!GrupaRepository.Data.ContainsKey(id))
                return NotFound();

            return Ok(GrupaRepository.Data[id]);
        }

        [HttpPost]
        public ActionResult<Grupa> AddGroup([FromBody] Grupa g)
        {
            if (GrupaRepository.Data.ContainsKey(g.Id))
                return Conflict("Grupa sa datim ID-jem već postoji.");

            GrupaRepository.Data[g.Id] = g;
            grupaRepository.Save();
            return CreatedAtAction(nameof(GetById), new { id = g.Id }, g);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!GrupaRepository.Data.ContainsKey(id))
                return NotFound();

            GrupaRepository.Data.Remove(id);
            grupaRepository.Save();
            return NoContent();
        }
    }
}
