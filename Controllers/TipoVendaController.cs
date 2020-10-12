using System.Collections.Generic;
using System.Threading.Tasks;
using PosMachine.Data;
using PosMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PosMachine.Controllers
{
    [ApiController]
    [Route("v1/tipos-de-venda")]
    public class TipoVendaController : ControllerBase
    {
        private async Task<bool> TipoVendaExistsAsync(int id, ApplicationContext context) =>
            await context.TiposVenda.AnyAsync(e => e.Id == id);

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<TipoVenda>>> Get([FromServices] ApplicationContext context)
        {
            var tiposDeVenda = await context.TiposVenda.ToListAsync();
            return tiposDeVenda;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<TipoVenda>> Get([FromServices] ApplicationContext context, int id)
        {
            var tipovenda = await context.TiposVenda
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return tipovenda;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TipoVenda>> Post(
            [FromServices] ApplicationContext context,
            [FromBody] TipoVenda model
        )
        {
            if (ModelState.IsValid)
            {
                context.TiposVenda.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(
            [FromServices] ApplicationContext context,
            [FromBody] TipoVenda model,
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
                    if (!await TipoVendaExistsAsync(id, context))
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
        public async Task<ActionResult<TipoVenda>> Delete(
            [FromServices]ApplicationContext context,
            int id)
        {
            var tipoVenda = await context.TiposVenda.FindAsync(id);
            if (tipoVenda == null)
            {
                return NotFound();
            }
            context.TiposVenda.Remove(tipoVenda);
            await context.SaveChangesAsync();

            return tipoVenda;
        }
    }
}