using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosMachine.Models
{
    public class Taxa
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(200, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        [MinLength(2, ErrorMessage = "Este campo deve conter entre 2 e 200 caracteres")]
        public string Nome { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Valor { get; set; } = 0;

    }
}