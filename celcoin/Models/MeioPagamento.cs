using System.ComponentModel.DataAnnotations;

namespace celcoin.Models
{
    public class MeioPagamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int TaxaId { get; set; }
        
        public Taxa Taxa { get; set; }

    }
}