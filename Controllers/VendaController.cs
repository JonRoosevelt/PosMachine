using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosMachine.Data;
using PosMachine.Models;
using PosMachine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using PosMachine.Exceptions;

namespace PosMachine.Controllers
{
    [ApiController]
    [Route("v1/venda")]
    public class VendaController : ControllerBase
    {
        private async Task<bool> VendaExistsAsync(int id, ApplicationContext context) =>
            await context.Vendas.AnyAsync(e => e.Id == id);

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
                try
                {
                    var meioPagamento = await context.MeiosPagamento
                        .Include(x => x.Taxa)
                        .FirstOrDefaultAsync(x => x.Id == model.MeioPagamentoId);
                    var taxaParcela = await context.Taxas
                        .FirstOrDefaultAsync(x => x.Id == model.TaxaParcelaId);
                    var tipoVenda = await context.TiposVenda
                        .FirstOrDefaultAsync(x => x.Id == model.TipoVendaId);

                    model.MeioPagamento = meioPagamento;
                    model.TaxaParcela = taxaParcela;
                    model.TipoVenda = tipoVenda;
                    var vendaService = new VendaService(model);
                    model.Recebivel = vendaService.CalcularRecebivel();
                    context.Vendas.Add(model);
                    var savedModel = await context.SaveChangesAsync();
                    return model;
                    
                }
                catch (PagamentoException exc)
                {
                    ModelState.AddModelError("Meio de Pagamento", exc.Message);
                    return BadRequest(ModelState);
                }
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
            [FromBody] Venda model,
            int id
        )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var meioPagamento = await context.MeiosPagamento
                        .Include(x => x.Taxa)
                        .FirstOrDefaultAsync(x => x.Id == model.MeioPagamentoId);
                    var taxaParcela = await context.Taxas
                        .FirstOrDefaultAsync(x => x.Id == model.TaxaParcelaId);
                    var tipoVenda = await context.TiposVenda
                        .FirstOrDefaultAsync(x => x.Id == model.TipoVendaId);

                    model.MeioPagamento = meioPagamento;
                    model.TaxaParcela = taxaParcela;
                    model.TipoVenda = tipoVenda;
                    var vendaService = new VendaService(model);
                    model.Recebivel = vendaService.CalcularRecebivel();

                    model.Id = id;
                    context.Entry(model).State = EntityState.Modified;
                }
                catch (PagamentoException exc)
                {
                    ModelState.AddModelError("Meio de Pagamento", exc.Message);
                    return BadRequest(ModelState);
                }

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await VendaExistsAsync(id, context))
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
        public async Task<ActionResult<Venda>> Delete(
            [FromServices]ApplicationContext context,
            int id)
        {
            var venda = await context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            context.Vendas.Remove(venda);
            await context.SaveChangesAsync();

            return venda;
        }

    }
}