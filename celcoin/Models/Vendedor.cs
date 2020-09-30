using System.ComponentModel.DataAnnotations;

namespace celcoin.Models
{
    public class Vendedor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        [MinLength(2, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        public string Nome { get; set; }
        public double Saldo { get; set; }

    }
}