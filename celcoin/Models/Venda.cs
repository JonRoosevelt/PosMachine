using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace celcoin.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; private set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]

        public int VendedorId { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]

        public int MeioPagamentoId { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int TipoVendaId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int TaxaParcelaId { get; set; }

        [Range(1, 12, ErrorMessage = "Numero de parcelas deve estar entre 1 e 12")]
        public int NumParcelas { get; set; } = 1;

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Recebivel { get; set; }


        public Vendedor Vendedor { get; set; }
        public MeioPagamento MeioPagamento { get; set; }
        public TipoVenda TipoVenda { get; set; }
        public Taxa TaxaParcela { get; set; }
        public DateTime Data { get; private set; } = new DateTime();
    }
}