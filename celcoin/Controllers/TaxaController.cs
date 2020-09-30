using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using celcoin.Models;
using celcoin.Data;

namespace celcoin.Controllers
{
    [ApiController]
    [Route("v1/taxas")]
    public class TaxaController : ControllerBase
    {
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


    }
}