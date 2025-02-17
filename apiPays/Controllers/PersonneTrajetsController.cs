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
    public class PersonneTrajetsController : ControllerBase
    {
        private readonly ContextePays _context;

        public PersonneTrajetsController(ContextePays context)
        {
            _context = context;
        }

        // GET: api/PersonneTrajets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonneTrajet>>> GetPersonneTrajets()
        {
            return await _context.PersonneTrajets.ToListAsync();
        }

        // GET: api/PersonneTrajets/5
        [HttpGet("{idPerson}-{idTrajet}")]
        public async Task<ActionResult<PersonneTrajet>> GetPersonneTrajet(int idPerson,int idTrajet)
        {
            var personneTrajet = await _context.PersonneTrajets.FindAsync(idPerson, idTrajet);

            if (personneTrajet == null)
            {
                return NotFound();
            }

            return personneTrajet;
        }

        // PUT: api/PersonneTrajets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idPerson}-{idTrajet}")]
        public async Task<IActionResult> PutPersonneTrajet(int idPerson,int idTrajet, PersonneTrajet personneTrajet)
        {
            if (idPerson != personneTrajet.PersonneId || idTrajet!= personneTrajet.TrajetId)
            {
                return BadRequest();
            }

            _context.Entry(personneTrajet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonneTrajetExists(idPerson,idTrajet))
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

        // POST: api/PersonneTrajets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonneTrajet>> PostPersonneTrajet(PersonneTrajet personneTrajet)
        {
            _context.PersonneTrajets.Add(personneTrajet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonneTrajetExists(personneTrajet.PersonneId,personneTrajet.TrajetId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonneTrajet", new { idPerson = personneTrajet.PersonneId,idTrajet=personneTrajet.TrajetId }, personneTrajet);
        }

        // DELETE: api/PersonneTrajets/5
        [HttpDelete("{idPerson}-{idTrajet}")]
        public async Task<IActionResult> DeletePersonneTrajet(int idPerson,int idTrajet)
        {
            var personneTrajet = await _context.PersonneTrajets.FindAsync(idPerson,idTrajet);
            if (personneTrajet == null)
            {
                return NotFound();
            }

            _context.PersonneTrajets.Remove(personneTrajet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonneTrajetExists(int idPerson,int idTrajet)
        {
            return _context.PersonneTrajets.Any(e => e.PersonneId == idPerson && e.TrajetId==idTrajet);
        }
    }
}
