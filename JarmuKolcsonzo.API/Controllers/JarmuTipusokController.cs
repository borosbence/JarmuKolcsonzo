using JarmuKolcsonzo.Model;
using JarmuKolcsonzo.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JarmuKolcsonzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JarmuTipusokController : ControllerBase
    {
        private readonly JKContext _context;

        public JarmuTipusokController(JKContext context)
        {
            _context = context;
        }

        // GET: api/JarmuTipusok
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JarmuTipus>>> Getjarmu_tipusok()
        {
            return await _context.jarmu_tipusok.OrderBy(x => x.megnevezes).ToListAsync();
        }

        // GET: api/JarmuTipusok/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JarmuTipus>> GetJarmuTipus(int id)
        {
            var jarmuTipus = await _context.jarmu_tipusok.FindAsync(id);

            if (jarmuTipus == null)
            {
                return NotFound();
            }

            return jarmuTipus;
        }

        // PUT: api/JarmuTipusok/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJarmuTipus(int id, JarmuTipus jarmuTipus)
        {
            if (id != jarmuTipus.id)
            {
                return BadRequest();
            }

            _context.Entry(jarmuTipus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JarmuTipusExists(id))
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

        // POST: api/JarmuTipusok
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JarmuTipus>> PostJarmuTipus(JarmuTipus jarmuTipus)
        {
            _context.jarmu_tipusok.Add(jarmuTipus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJarmuTipus", new { id = jarmuTipus.id }, jarmuTipus);
        }

        // DELETE: api/JarmuTipusok/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJarmuTipus(int id)
        {
            var jarmuTipus = await _context.jarmu_tipusok.FindAsync(id);
            if (jarmuTipus == null)
            {
                return NotFound();
            }

            _context.jarmu_tipusok.Remove(jarmuTipus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JarmuTipusExists(int id)
        {
            return _context.jarmu_tipusok.Any(e => e.id == id);
        }
    }
}
