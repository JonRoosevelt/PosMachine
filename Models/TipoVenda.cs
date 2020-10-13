using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosMachine.Models
{
    public class TipoVenda
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        [MinLength(2, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        public string Nome { get; set; }
    }
}