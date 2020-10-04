using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celcoin.Data;
using celcoin.Models;

namespace celcoin.Controllers
{
    [ApiController]
    [Route("v1/meios-de-pagamento")]
    public class MeioPagamentoController : ControllerBase
    {
        private async Task<bool> MeioPagamentoExistsAsync(int id, ApplicationContext context) =>
            await context.MeiosPagamento.AnyAsync(e => e.Id == id);

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<MeioPagamento>>> Get([FromServices] ApplicationContext context)
        {
            var meiosPagamento = await context.MeiosPagamento
                .AsNoTracking()
                .Include(x => x.Taxa)
                .ToListAsync();
            return Ok(meiosPagamento);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<MeioPagamento>> GetById(
            [FromServices] ApplicationContext context, int id)
        {
            var meioPagamento = await context.MeiosPagamento
                .AsNoTracking()
                .Include(x => x.Taxa)
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(meioPagamento);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<MeioPagamento>> Post(
            [FromServices] ApplicationContext context,
            [FromBody] MeioPagamento model
        )
        {
            if (ModelState.IsValid)
            {
                context.MeiosPagamento.Add(model);
                await context.SaveChangesAsync();
                return Created("", model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(
            [FromServices]ApplicationContext context,
            [FromBody]MeioPagamento model,
            int id
        )
        {
            if (ModelState.IsValid)
            {
                model.Id = id;
                context.Entry(model).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MeioPagamentoExistsAsync(id, context))
                    {
                        return NotFound();
                    }
                    else 
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<MeioPagamento>> Delete(
            [FromServices]ApplicationContext context,
            int id)
        {
            var meioPagamento = await context.MeiosPagamento.FindAsync(id);
            if (meioPagamento == null)
            {
                return NotFound();
            }
            context.MeiosPagamento.Remove(meioPagamento);
            await context.SaveChangesAsync();

            return meioPagamento;
        }
    }
}