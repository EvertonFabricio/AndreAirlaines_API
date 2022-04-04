using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreAirlaines_API.Data;
using AndreAirlaines_API.Model;

namespace AndreAirlaines_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoosController : ControllerBase
    {
        private readonly AndreAirlaines_APIContext _context;

        public VoosController(AndreAirlaines_APIContext context)
        {
            _context = context;
        }

        // GET: api/Voos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voo>>> GetVoo()
        {
            return await _context.Voo.Include(aeroporto =>aeroporto.Origem)
                                     .Include(aeroporto => aeroporto.Origem.Endereco)
                                     .Include(aeroporto => aeroporto.Destino)
                                     .Include(aeroporto => aeroporto.Destino.Endereco)
                                     .Include(aeronave => aeronave.Aeronave)
                                     .ToListAsync();
        }

        // GET: api/Voos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voo>> GetVoo(int id)
        {
            var voo = await _context.Voo.Include(aeroporto => aeroporto.Origem)
                                        .Include(aeroporto => aeroporto.Origem.Endereco)
                                        .Include(aeroporto => aeroporto.Destino)
                                        .Include(aeroporto => aeroporto.Destino.Endereco)
                                        .Include(aeronave => aeronave.Aeronave)
                                        .Where(busca => busca.Id == id)
                                        .SingleOrDefaultAsync();
                                     

            if (voo == null)
            {
                return NotFound();
            }

            return voo;
        }

        // PUT: api/Voos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoo(int id, Voo voo)
        {
            if (id != voo.Id)
            {
                return BadRequest();
            }

            _context.Entry(voo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VooExists(id))
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

        // POST: api/Voos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voo>> PostVoo(Voo voo)
        {

            var destino = await _context.Aeroporto.Where(aeroporto => aeroporto.Sigla == voo.Destino.Sigla).SingleOrDefaultAsync();
            var origem = await _context.Aeroporto.Where(aeroporto => aeroporto.Sigla == voo.Origem.Sigla).SingleOrDefaultAsync();
            var aeronave = await _context.Aeronave.Where(aeronave => aeronave.Id == voo.Aeronave.Id).SingleOrDefaultAsync();

            voo.Aeronave = aeronave;
            voo.Destino = destino;
            voo.Origem = origem;


            _context.Voo.Add(voo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoo", new { id = voo.Id }, voo);
        }

        // DELETE: api/Voos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoo(int id)
        {
            var voo = await _context.Voo.FindAsync(id);
            if (voo == null)
            {
                return NotFound();
            }

            _context.Voo.Remove(voo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VooExists(int id)
        {
            return _context.Voo.Any(e => e.Id == id);
        }
    }
}
