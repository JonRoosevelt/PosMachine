using System.Collections.Generic;
using PosMachine.Models;
using System;
using PosMachine.Exceptions;

namespace PosMachine.Services
{
    public class VendaService
    {
        public VendaService(Venda venda)
        {
            this.venda = venda;
        }

        private Venda venda { get; set; }

        public decimal CalcularRecebivel()
        {
            if (venda.MeioPagamento.Nome == "Débito")
            {
                if (venda.TipoVenda.Nome == "PARCELADO_CLIENTE" ||
                        venda.NumParcelas > 1)  
                    {
                        throw new PagamentoException(
                            "Pagamento em Débito não aceita parcelamento."
                        );
                    }
                if (venda.TaxaParcela.Nome != "taxa_parcela_debito")
                {
                    throw new PagamentoException(
                            "Taxa de parcela deve ser 'taxa_parcela_debito'."
                        );
                }
            } 
            if (venda.MeioPagamento.Nome == "Crédito" &&
                    venda.TaxaParcela.Nome != "taxa_parcela_credito" &&
                    venda.TaxaParcela.Nome != "taxa_parcela_debito")
                    {
                        throw new PagamentoException(
                        "Somente aceitas taxas de parcelamento em crédito ou débito."
                        );
                    }
            switch (venda.TipoVenda.Nome)
            {
                case "CUSTO_VENDEDOR":
                    return calcularCustoVendedor();
                case "PARCELADO_CLIENTE":
                    return calcularParceladoCliente();
                case "CUSTO_CLIENTE":
                    return calcularCustoCliente();
                default:
                    return venda.ValorVenda;
            }
        }

        private decimal calcularCustoCliente()
        {
            var numParcelas = 1;
            if (venda.NumParcelas > 0)
            {
                numParcelas = venda.NumParcelas;
            }
            var vendaComTaxaPagamento = venda.ValorVenda * (venda.MeioPagamento.Taxa.Valor / 100 + 1);
            var recebivel = 0.0m;
            if (numParcelas > 1)
            {
                var valorParcela = vendaComTaxaPagamento / numParcelas * venda.TaxaParcela.Valor;
                recebivel = valorParcela * numParcelas;
            }
            else {
                recebivel = vendaComTaxaPagamento;
            }
            return recebivel;
        }

        private decimal calcularParceladoCliente()
        {
            var numParcelas = 1;
            if (venda.NumParcelas > 0)
            {
                numParcelas = venda.NumParcelas;
            }
            var valorParcela = venda.ValorVenda / numParcelas;
            var valorParcelaComTaxa = valorParcela / 1 - venda.TaxaParcela.Valor;
            var recebivel = valorParcelaComTaxa * numParcelas;
            return recebivel;
        }

        private decimal calcularCustoVendedor()
        {
            var taxaTotal = calcularTaxas();
            var recebivel = venda.ValorVenda / 1 - taxaTotal;
            return recebivel;
        }

        private decimal calcularTaxas()
        {
            var taxas = venda.MeioPagamento.Taxa.Valor;
            if (venda.MeioPagamento.Nome == "Débito")
            {
                return taxas;
            }
            else
            {
                var taxaParcelamento = venda.NumParcelas * venda.TaxaParcela.Valor;
                taxas += taxaParcelamento;
            }
            return taxas;
        }
        
    }
}