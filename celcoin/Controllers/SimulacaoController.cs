using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using celcoin.Data;
using celcoin.Exceptions;
using celcoin.Models;
using celcoin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace celcoin.Controllers
{
    [Produces("application/json")]   
    [ApiController]
    [Route("v1/simular")]
    public class SimulacaoController : Controller
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Venda>>> Simulate(
            [FromServices] ApplicationContext context,
            [FromBody] Venda model
        )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var models = new List<Venda>();

                    var tiposVenda = await context.TiposVenda
                        .AsNoTracking()
                        .ToListAsync();

                    var meioPagamento = await context.MeiosPagamento
                        .AsNoTracking()
                        .Include(x => x.Taxa)
                        .FirstOrDefaultAsync(x => x.Id == model.MeioPagamentoId);

                    var taxaParcela = await context.Taxas
                        .FirstOrDefaultAsync(x => x.Id == model.TaxaParcelaId);

                    foreach(var tipo in tiposVenda)
                    {
                        model.TipoVenda = tipo;
                        model.MeioPagamento = meioPagamento;
                        model.TaxaParcela = taxaParcela;

                        var vendaService1 = new VendaService(model);

                        model.Recebivel = vendaService1.CalcularRecebivel();

                        context.Vendas.Add((Venda)model);
                        models.Add(model);
                    }
                    return models;

                }
                catch (PagamentoException exc)
                {
                    ModelState.AddModelError("Meio de Pagamento", exc.Message);
                    return BadRequest(ModelState);
                }
                catch(Exception exc)
                {
                    ModelState.AddModelError("Erro", exc.Message);
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}