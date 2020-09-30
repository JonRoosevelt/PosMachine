using System.ComponentModel.DataAnnotations;

namespace celcoin.Models
{
    public class TipoVenda
    {

        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        [MinLength(2, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        public string nome { get; set; }
    }
}