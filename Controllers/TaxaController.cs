using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosMachine.Models;
using PosMachine.Data;

namespace PosMachine.Controllers
{
    [ApiController]
    [Route("v1/taxas")]
    public class TaxaController : ControllerBase
    {
        private async Task<bool> TaxaExistsAsync(int id, ApplicationContext context) =>
            await context.Taxas.AnyAsync(e => e.Id == id);

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Taxa>>> Get([FromServices] ApplicationContext context)
        {
            var taxas = await context.Taxas
                .AsNoTracking()
                .ToListAsync();
            return taxas;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Taxa>> Get([FromServices] ApplicationContext context, int id)
        {
            var taxa = await context.Taxas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return taxa;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Taxa>> Post(
            [FromServices] ApplicationContext context,
            [FromBody]Taxa model
        )
        {
            if (ModelState.IsValid)
            {
                context.Taxas.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Put(
            [FromServices] ApplicationContext context,
            [FromBody] Taxa model,
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
                    if (!await TaxaExistsAsync(id, context))
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
        public async Task<ActionResult<Taxa>> Delete(
            [FromServices]ApplicationContext context,
            int id)
        {
            var taxa = await context.Taxas.FindAsync(id);
            if (taxa == null)
            {
                return NotFound();
            }
            context.Taxas.Remove(taxa);
            await context.SaveChangesAsync();

            return taxa;
        }
    }
}