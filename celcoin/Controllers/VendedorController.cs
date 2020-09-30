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
    }
}