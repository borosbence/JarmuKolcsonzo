using JarmuKolcsonzo.Model;
using JarmuKolcsonzo.Model.DTOs;
using JarmuKolcsonzo.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JarmuKolcsonzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JarmuvekController : ControllerBase
    {
        private readonly JKContext _context;

        public JarmuvekController(JKContext context)
        {
            _context = context;
        }

        // GET: api/Jarmuvek
        [HttpGet]
        public async Task<TableDTO<Jarmu>> GetAll(
            int page = 1,
            int itemsPerPage = 20,
            string? search = null,
            string? sortBy = null,
            bool ascending = true)
        {
            var query = _context.jarmuvek.Include(x => x.tipus).OrderBy(x => x.rendszam).AsQueryable();
            // Keresés
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                // Ha a keresési kulcsszó szám
                int.TryParse(search, out int dij);
                // Ha dátum
                DateTime.TryParse(search, out DateTime datum);

                query = query.Where(x =>
                    x.rendszam.ToLower().Contains(search) ||
                    x.dij.Equals(dij) ||
                    x.tipus.megnevezes.ToLower().Contains(search) ||
                    x.szerviz_datum.Equals(datum));
            }

            // Sorbarendezés
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "dij":
                        query = ascending ? query.OrderBy(x => x.dij) : query.OrderByDescending(x => x.dij);
                        break;
                    case "elerheto":
                        query = ascending ? query.OrderBy(x => x.elerheto) : query.OrderByDescending(x => x.elerheto);
                        break;
                    case "szerviz_datum":
                        query = ascending ? query.OrderBy(x => x.szerviz_datum) : query.OrderByDescending(x => x.szerviz_datum);
                        break;
                    case "tipus.megnevezes":
                        query = ascending ? query.OrderBy(x => x.tipus.megnevezes) : query.OrderByDescending(x => x.tipus.megnevezes);
                        break;
                    default:
                        query = ascending ? query.OrderBy(x => x.rendszam) : query.OrderByDescending(x => x.rendszam);
                        break;
                }
            }

            // Összes találat kiszámítása
            int totalItems = await query.CountAsync();

            // Oldaltördelés
            if (page + itemsPerPage > 0)
            {
                query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }
            var data = await query.ToListAsync();
            return new TableDTO<Jarmu>(data, totalItems);
        }

        // GET: api/Jarmuvek/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jarmu>> GetJarmu(int id)
        {
            var jarmu = await _context.jarmuvek.FindAsync(id);

            if (jarmu == null)
            {
                return NotFound();
            }

            return jarmu;
        }

        // PUT: api/Jarmuvek/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJarmu(int id, Jarmu jarmu)
        {
            if (id != jarmu.id)
            {
                return BadRequest();
            }

            // Szükséges, hogy ne módosítson járműtípust
            jarmu.tipus = null;

            _context.Entry(jarmu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JarmuExists(id))
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

        // POST: api/Jarmuvek
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jarmu>> PostJarmu(Jarmu jarmu)
        {
            // Szükséges, hogy ne adjon hozzá új járműtípust
            jarmu.tipus = null;
            _context.jarmuvek.Add(jarmu);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetJarmu", new { id = jarmu.id }, jarmu);
        }

        // DELETE: api/Jarmuvek/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJarmu(int id)
        {
            var jarmu = await _context.jarmuvek.FindAsync(id);
            if (jarmu == null)
            {
                return NotFound();
            }

            _context.jarmuvek.Remove(jarmu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JarmuExists(int id)
        {
            return _context.jarmuvek.Any(e => e.id == id);
        }
    }
}
