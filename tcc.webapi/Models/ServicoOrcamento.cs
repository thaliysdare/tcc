using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc.webapi.Models
{
    [Table("ServicoOrcamento")]
    public class ServicoOrcamento : GenericoModel
    {
        public int ServicoOrcamentoId { get; set; }

        [Required]
        [ForeignKey(nameof(Servico))]
        public int ServicoId { get; set; }

        [Required]
        [ForeignKey(nameof(Orcamento))]
        public int OrcamentoId { get; set; }

        [Required]
        public double Valor { get; set; }

        public virtual Servico Servico { get; set; }
        public virtual Orcamento Orcamento { get; set; }
    }
}
