using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celcoin.Data;
using celcoin.Models;
namespace celcoin.Controllers
{
    [ApiController]
    [Route("v1/vendedores")]
    public class VendedorController : ControllerBase
    {
        private async Task<bool> VendedorExistsAsync(int id, ApplicationContext context) =>
            await context.Vendedores.AnyAsync(e => e.Id == id);

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Vendedor>>> Get([FromServices] ApplicationContext context)
        {
            var vendedores = await context.Vendedores.ToListAsync();
            return vendedores;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Vendedor>> Get([FromServices] ApplicationContext context, int id)
        {
            var vendedor = await context.Vendedores
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return vendedor;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Vendedor>> Post(
            [FromServices] ApplicationContext context,
            [FromBody]Vendedor model
        )
        {
            if (ModelState.IsValid)
            {
                context.Vendedores.Add(model);
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
            [FromBody] Vendedor model,
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
                    if (!await VendedorExistsAsync(id, context))
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
        public async Task<ActionResult<Vendedor>> Delete(
            [FromServices]ApplicationContext context,
            int id)
        {
            var vendedor = await context.Vendedores.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            context.Vendedores.Remove(vendedor);
            await context.SaveChangesAsync();

            return vendedor;
        }
    }
}