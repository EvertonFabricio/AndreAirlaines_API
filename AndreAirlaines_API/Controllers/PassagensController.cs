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
    public class PassagensController : ControllerBase
    {
        private readonly AndreAirlaines_APIContext _context;

        public PassagensController(AndreAirlaines_APIContext context)
        {
            _context = context;
        }

        // GET: api/Passagens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passagem>>> GetPassagem()
        {
            return await _context.Passagem.Include(passsagem => passsagem.Voo.Aeronave)
                                          .Include(passsagem => passsagem.Voo.Origem.Endereco)
                                          .Include(passsagem => passsagem.Voo.Destino.Endereco)
                                          .Include(passsagem => passsagem.Classe)
                                          .Include(passsagem => passsagem.Passageiro.Endereco)
                                          .ToListAsync();
        }

        // GET: api/Passagens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passagem>> GetPassagem(int id)
        {
            var passagem = await _context.Passagem.Include(passsagem => passsagem.Voo.Aeronave)
                                          .Include(passsagem => passsagem.Voo.Origem.Endereco)
                                          .Include(passsagem => passsagem.Voo.Destino.Endereco)
                                          .Include(passsagem => passsagem.Classe)
                                          .Include(passsagem => passsagem.Passageiro.Endereco)
                                          .Where(passsagem => passsagem.Id == id)
                                          .SingleOrDefaultAsync();

            if (passagem == null)
            {
                return NotFound();
            }

            return passagem;
        }

        // PUT: api/Passagens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassagem(int id, Passagem passagem)
        {
            if (id != passagem.Id)
            {
                return BadRequest();
            }

            _context.Entry(passagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassagemExists(id))
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

        // POST: api/Passagens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passagem>> PostPassagem(Passagem passagem)
        {
           
            var precoVoo = await _context.PrecoBase.Where(preco => preco.Origem == passagem.Voo.Origem)
                                                .SingleOrDefaultAsync();

            //ta tudo supostamente pronto. Eu só não sei comparar origem e destino da passagem com origem e destino do PrecoBase.
            //eu preciso da comparaçao simultanea, e não separada como eu fiz.

            passagem.Valor = precoVoo.Preco;


            switch (passagem.Classe.Id)
            {

                case 1: //normal
                    if (passagem.Desconto > 0)
                    {
                        passagem.Valor -= passagem.Valor * passagem.Desconto / 100;
                    }
                    break;


                case 2: //economica
                    passagem.Valor *= 0.85;  //15% mais barato 
                    if (passagem.Desconto > 0)
                    {
                        passagem.Valor = passagem.Valor - (passagem.Valor * passagem.Desconto / 100);
                    }

                    break;

                case 3: //primeira classe
                    passagem.Valor *= 1.2; //20% mais caro
                    if (passagem.Desconto > 0)
                    {
                        passagem.Valor -= passagem.Valor * passagem.Desconto / 100;
                    }
                    break;

                case 4: //quase na asa
                    passagem.Valor *= 0.7; //30% mais barato
                    if (passagem.Desconto > 0)
                    {
                        passagem.Valor -= passagem.Valor * passagem.Desconto / 100;
                    }
                    break;

                default:
                    throw new Exception("Classe não cadastrada");
            }

            _context.Passagem.Add(passagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassagem", new { id = passagem.Id }, passagem);
        }

        // DELETE: api/Passagens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassagem(int id)
        {
            var passagem = await _context.Passagem.FindAsync(id);
            if (passagem == null)
            {
                return NotFound();
            }

            _context.Passagem.Remove(passagem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassagemExists(int id)
        {
            return _context.Passagem.Any(e => e.Id == id);
        }
    }
}
