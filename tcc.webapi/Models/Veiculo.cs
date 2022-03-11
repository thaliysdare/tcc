using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc.webapi.Models
{
    [Table("Veiculo")]
    public class Veiculo
    {
        public int VeiculoId { get; set; }

        [Required]
        [ForeignKey(nameof(Cliente))]
        public int ClienteId { get; set; }

        [Required]
        public string Placa { get; set; }

        public string Marca { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
