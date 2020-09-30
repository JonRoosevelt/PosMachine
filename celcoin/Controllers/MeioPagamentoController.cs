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
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<MeioPagamento>>> Get([FromServices] ApplicationContext context)
        {
            var meiosPagamento = await context.MeiosPagamento
                .AsNoTracking()
                .Include(x => x.Taxa)
                .ToListAsync();
            return meiosPagamento;
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
            return meioPagamento;
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
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }
}