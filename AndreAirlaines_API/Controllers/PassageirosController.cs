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
    public class PassageirosController : ControllerBase
    {
        private readonly AndreAirlaines_APIContext _context;

        public PassageirosController(AndreAirlaines_APIContext context)
        {
            _context = context;
        }

        // GET: api/Passageiros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passageiro>>> GetPassageiro()
        {
           return await _context.Passageiro.Include(e => e.Endereco).ToListAsync();

            //return await _context.Passageiro.ToListAsync(); // essa era a linha original
                                                             //aqui muda tbm, coloca .Include(e => e.endereco) mantem o to list async
        }

        // GET: api/Passageiros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passageiro>> GetPassageiro(string id)
        {
           var passageiro = await _context.Passageiro.Include(e => e.Endereco)
                                                     .Where(d => d.Cpf == id)
                                                     .SingleOrDefaultAsync();
            
            //var passageiro = await _context.Passageiro.FindAsync(id);   //Linha original. É aqui que inclui a chamada do endereço.
            //no lugar do find async faz um lambda com .include
            //fica assim:
            //.Include(e => e.endereco).ToListAsync.Where(d =>d.Id = id).SingleOrDefaultAsync();


            if (passageiro == null)
            {
                return NotFound();
            }

            return passageiro;
        }

        // PUT: api/Passageiros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassageiro(string id, Passageiro passageiro)
        {
            if (id != passageiro.Cpf)
            {
                return BadRequest();
            }

            _context.Entry(passageiro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassageiroExists(id))
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

        // POST: api/Passageiros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passageiro>> PostPassageiro(Passageiro passageiro)
        {
            //****aqui é onde eu puxo o endereco que ja existe pra colocar no cliente que eu cadastrei.
           
            var endereco = await _context.Endereco.Where(x => x.Id == passageiro.Endereco.Id).FirstOrDefaultAsync();
            var enderecoSite = await BuscaCep.BuscaCep.ViaCep(passageiro.Endereco.Cep);


            if (endereco != null)
            {
            passageiro.Endereco = endereco;
            }
            else if (enderecoSite != null)
            {
                passageiro.Endereco.Logradouro = enderecoSite.Logradouro;
                passageiro.Endereco.Localidade = enderecoSite.Localidade;
                passageiro.Endereco.Uf = enderecoSite.Uf;
                passageiro.Endereco.Bairro = enderecoSite.Bairro;
            
            }
             // daqui pra baixo ja tava feito.
            _context.Passageiro.Add(passageiro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PassageiroExists(passageiro.Cpf))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPassageiro", new { id = passageiro.Cpf }, passageiro);
        }

        // DELETE: api/Passageiros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassageiro(string id)
        {
            var passageiro = await _context.Passageiro.FindAsync(id);
            if (passageiro == null)
            {
                return NotFound();
            }

            _context.Passageiro.Remove(passageiro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassageiroExists(string id)
        {
            return _context.Passageiro.Any(e => e.Cpf == id);
        }
    }
}
