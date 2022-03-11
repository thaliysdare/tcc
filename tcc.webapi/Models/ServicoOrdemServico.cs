using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc.webapi.Models
{
    [Table("ServicoOrdemServico")]
    public class ServicoOrdemServico : GenericoModel
    {
        public int ServicoOrdemServicoId { get; set; }

        [Required]
        [ForeignKey(nameof(Servico))]
        public int ServicoId { get; set; }

        [Required]
        [ForeignKey(nameof(OrdemServico))]
        public int OrdemServicoId { get; set; }

        [Required]
        public double Valor { get; set; }

        public virtual Servico Servico { get; set; }
        public virtual OrdemServico OrdemServico { get; set; }
    }
}
