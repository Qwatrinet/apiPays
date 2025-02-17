using ApiPays.DB;
using ApiPays.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPays.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {


        private readonly ContextePays _context;

        public VillesController(ContextePays context)
        {
            _context = context;
        }

        // GET: api/Villes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ville>>> GetVille_1()
        {
            return await _context.Villes
                .ToListAsync();
        }

        // GET: api/Villes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ville>> GetVille(int id)
        {
            var ville = await _context.Villes.FindAsync(id);

            if (ville == null)
            {
                return NotFound();
            }

            return ville;
        }

        // GET: api/Villes/Country/5
        [HttpGet("Country/{paysId}")]
        public async Task<ActionResult<IEnumerable<Ville>>> GetVilleOfCountry(int paysId)
        {
            var pays = await _context.Pays.FindAsync(paysId);

            if (pays == null)
            {
                return NotFound();
            }

            {
                return await _context.Villes.Where(v => v.PaysId == paysId)
                    .ToListAsync();
            }
        }

        // PUT: api/Villes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVille(int id, Ville ville)
        {
            if (id != ville.Id)
            {
                return BadRequest();
            }

            _context.Entry(ville).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VilleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Villes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ville>> PostVille(Ville ville)
        {
            if (_context.Pays.Find(ville.PaysId) == null)
            {
                return BadRequest();
            }

            if (_context.Villes.ToList().Any(v => v.NomVille == ville.NomVille && v.CodePostal == ville.CodePostal))
            {
                return Conflict(new { type = "https://tools.ietf.org/html/rfc9110#section-15.5.10", title = "Conflit, doublon détecté", status = 409 });
            }

            _context.Villes.Add(ville);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVille", new { id = ville.Id }, ville);
        }

        // DELETE: api/Villes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVille(int id)
        {
            var ville = await _context.Villes.FindAsync(id);
            if (ville == null)
            {
                return NotFound();
            }

            _context.Villes.Remove(ville);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VilleExists(int id)
        {
            return _context.Villes.Any(e => e.Id == id);
        }
    }
}
