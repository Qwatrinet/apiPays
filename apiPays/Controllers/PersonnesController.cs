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
    public class PersonnesController : ControllerBase
    {
        private readonly ContextePays _context;

        public PersonnesController(ContextePays context)
        {
            _context = context;
        }

        // GET: api/Personnes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        {
            return await _context.Personnes.ToListAsync();
        }

        // GET: api/Personnes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personne>> GetPersonne(int id)
        {
            var personne = await _context.Personnes.FindAsync(id);

            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        // PUT: api/Personnes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonne(int id, Personne personne)
        {
            if (id != personne.Id)
            {
                return BadRequest();
            }

            _context.Entry(personne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonneExists(id))
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

        // POST: api/Personnes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Personne>> PostPersonne(Personne personne)
        {
            _context.Personnes.Add(personne);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonne", new { id = personne.Id }, personne);
        }

        // DELETE: api/Personnes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonne(int id)
        {
            var personne = await _context.Personnes.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }

            if (_context.PersonneTrajets.FirstOrDefault(p => p.PersonneId == id)!=null){
                return Conflict(new {error="La personne est associée à des trajets et ne peut pas être supprimée!"});
            }

            _context.Personnes.Remove(personne);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonneExists(int id)
        {
            return _context.Personnes.Any(e => e.Id == id);
        }
    }
}
