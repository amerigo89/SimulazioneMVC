using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulazioneMVC.Model;

namespace SimulazioneMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrazionisController : ControllerBase
    {
        private readonly AutoriContext _context;

        public RegistrazionisController(AutoriContext context)
        {
            _context = context;
        }

        // GET: api/Registrazionis
        [HttpGet]
        public IEnumerable<Registrazioni> GetRegistrazioni()
        {
            return _context.Registrazioni;
        }

        [HttpGet]
        public IEnumerable<Registrazioni> SearchByAutoreId(int id)
        {
            return _context.Registrazioni.Where(r => r.IdAutore == id);
        }

        // GET: api/Registrazionis/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistrazioni([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registrazioni = await _context.Registrazioni.FindAsync(id);

            if (registrazioni == null)
            {
                return NotFound();
            }

            return Ok(registrazioni);
        }

        // PUT: api/Registrazionis/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistrazioni([FromRoute] int id, [FromBody] Registrazioni registrazioni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registrazioni.IdPresentazione)
            {
                return BadRequest();
            }

            _context.Entry(registrazioni).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrazioniExists(id))
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

        // POST: api/Registrazionis
        [HttpPost]
        public async Task<IActionResult> PostRegistrazioni([FromBody] Registrazioni registrazioni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Registrazioni.Add(registrazioni);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RegistrazioniExists(registrazioni.IdPresentazione))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRegistrazioni", new { id = registrazioni.IdPresentazione }, registrazioni);
        }

        // DELETE: api/Registrazionis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistrazioni([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registrazioni = await _context.Registrazioni.FindAsync(id);
            if (registrazioni == null)
            {
                return NotFound();
            }

            _context.Registrazioni.Remove(registrazioni);
            await _context.SaveChangesAsync();

            return Ok(registrazioni);
        }

        private bool RegistrazioniExists(int id)
        {
            return _context.Registrazioni.Any(e => e.IdPresentazione == id);
        }
    }
}