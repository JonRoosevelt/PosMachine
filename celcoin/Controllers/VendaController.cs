using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using celcoin.Data;
using celcoin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace celcoin.Controllers
{
    [ApiController]
    [Route("v1/venda")]
    public class VendaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Venda>>> Get([FromServices] ApplicationContext context)
        {
            var vendas = await context.Vendas
                .AsNoTracking()
                .Include(x => x.Vendedor)
                .Include(x => x.MeioPagamento)
                .Include(x => x.TipoVenda)
                .Include(x => x.TaxaParcela)
                .ToListAsync();
            return vendas;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Venda>> GetById(
            [FromServices] ApplicationContext context, int id)
        {
            var venda = await context.Vendas
                .AsNoTracking()
                .Include(x => x.Vendedor)
                .Include(x => x.MeioPagamento)
                .Include(x => x.TipoVenda)
                .Include(x => x.TaxaParcela)
                .FirstOrDefaultAsync(x => x.Id == id);
            return venda;
        }
        [HttpGet]
        [Route("tipos-de-venda/{id:int}")]
        public async Task<ActionResult<List<Venda>>> GetByTipo(
            [FromServices] ApplicationContext context, int id)
        {
            var vendas = await context.Vendas
                .Include(x => x.TipoVenda)
                .AsNoTracking()
                .Where(x => x.TipoVendaId == id)
                .ToListAsync();
            return vendas;
        }
    
        [HttpGet]
        [Route("vendedores/{id:int}")]
        public async Task<ActionResult<List<Venda>>> GetByVendedor(
            [FromServices] ApplicationContext context, int id)
        {
            var vendas = await context.Vendas
                .Include(x => x.Vendedor)
                .AsNoTracking()
                .Where(x => x.VendedorId == id)
                .ToListAsync();
            return vendas;
        }

        [HttpGet]
        [Route("meios-de-pagamento/{id:int}")]
        public async Task<ActionResult<List<Venda>>> GetByMeioPagamento(
            [FromServices] ApplicationContext context, int id)
        {
            var vendas = await context.Vendas
                .Include(x => x.MeioPagamento)
                .AsNoTracking()
                .Where(x => x.MeioPagamentoId == id)
                .ToListAsync();
            return vendas;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Venda>> Post(
            [FromServices] ApplicationContext context,
            [FromBody] Venda model
        )
        {
            if (ModelState.IsValid)
            {
                context.Vendas.Add(model);
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