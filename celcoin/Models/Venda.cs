using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace celcoin.Models
{
    public class Venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
        public decimal Recebivel { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal ValorVenda { get; set; }
        public Vendedor Vendedor { get; set; }
        public MeioPagamento MeioPagamento { get; set; }
        public TipoVenda TipoVenda { get; set; }
        public Taxa TaxaParcela { get; set; }
        public DateTime Data { get; private set; } = new DateTime();
    }
}