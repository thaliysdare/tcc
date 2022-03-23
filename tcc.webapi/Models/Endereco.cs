using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc.webapi.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        public int EnderecoId { get; set; }

        [Required]
        public string Rua { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }
    }
}
