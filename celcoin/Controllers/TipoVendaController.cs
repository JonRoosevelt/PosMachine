using System.Collections.Generic;
using System.Threading.Tasks;
using celcoin.Data;
using celcoin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace celcoin.Controllers
{
    [ApiController]
    [Route("v1/tipos-de-venda")]
    public class TipoVendaController : ControllerBase
    {
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
    }
}