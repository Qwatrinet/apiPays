using ApiPays.DB;
using ApiPays.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ApiPays.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaysController : ControllerBase
    {
        private readonly ContextePays _context;

        public PaysController(ContextePays context)
        {
            _context = context;
        }

        //POST: api/Pays/bulk
        [HttpPost("bulk", Name = "PostCountries")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Pays> PostCountries(IEnumerable<Pays> countries)
        {
            this._context.Pays.AddRange(countries);

            this._context.SaveChanges();

            return this.CreatedAtAction(nameof(this.PostCountries), new { count = countries.Count() }, countries);
        }


        // GET: api/Pays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pays>>> GetPays()
        {
            return await _context.Pays.Include(p => p.Villes).ToListAsync();
        }

        // GET: api/Pays/5
        [HttpGet("{id}")]
        public ActionResult<Pays> GetPays(int id)
        {
            var pays = _context.Pays.Include(p => p.Villes).ToList().Find(p => p.PaysId == id);

            if (pays == null)
            {
                return NotFound();
            }

            return pays;
        }

        // PUT: api/Pays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPays(int id, Pays pays)
        {
            if (id != pays.PaysId)
            {
                return BadRequest();
            }

            _context.Entry(pays).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaysExists(id))
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

        // POST: api/Pays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pays>> PostPays(Pays pays)
        {
            _context.Pays.Add(pays);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPays", new { id = pays.PaysId }, pays);
        }

        // DELETE: api/Pays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePays(int id)
        {
            var pays = await _context.Pays.FindAsync(id);
            if (pays == null)
            {
                return NotFound();
            }

            _context.Pays.Remove(pays);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaysExists(int id)
        {
            return _context.Pays.Any(e => e.PaysId == id);
        }
    }
}
