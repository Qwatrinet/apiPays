using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPays.DB;
using ApiPays.Models;

namespace ApiPays.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrajetsController : ControllerBase
    {
        private readonly ContextePays _context;

        public TrajetsController(ContextePays context)
        {
            _context = context;
        }

        // GET: api/Trajets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trajet>>> GetTrajets()
        {
            return await _context.Trajets.ToListAsync();
        }

        // GET: api/Trajets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trajet>> GetTrajet(int id)
        {
            var trajet = await _context.Trajets.FindAsync(id);

            if (trajet == null)
            {
                return NotFound();
            }

            return trajet;
        }

        // PUT: api/Trajets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrajet(int id, Trajet trajet)
        {
            if (id != trajet.Id)
            {
                return BadRequest();
            }
            if (trajet.VilleArriveeId == trajet.VilleDepartId)
            {
                return BadRequest(new { error = "La ville de départ est identique à celle d'arrivée." });
            }



            _context.Entry(trajet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrajetExists(id))
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

        // POST: api/Trajets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trajet>> PostTrajet(Trajet trajet)
        {
            //check if VilleArrivée!=VilleDépart
            if (trajet.VilleArriveeId == trajet.VilleDepartId)
            {
                return BadRequest(new { error ="La ville de départ est identique à celle d'arrivée."});
            }

            _context.Trajets.Add(trajet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrajet", new { id = trajet.Id }, trajet);
        }

        // DELETE: api/Trajets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrajet(int id)
        {
            var trajet = await _context.Trajets.FindAsync(id);
            if (trajet == null)
            {
                return NotFound();
            }

            _context.Trajets.Remove(trajet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrajetExists(int id)
        {
            return _context.Trajets.Any(e => e.Id == id);
        }
    }
}
