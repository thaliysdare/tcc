using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tcc.webapi.Enums;

namespace tcc.webapi.Models
{
    [Table("Servico")]
    public class Servico
    {
        public int ServicoId { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string DescricaoResumida { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public StatusServicoEnum IdcStatusServico { get; set; }

        public virtual ICollection<ServicoOrcamento> ServicoOrcamento { get; set; }
        public virtual ICollection<ServicoOrdemServico> ServicoOrdemServico { get; set; }
    }
}
