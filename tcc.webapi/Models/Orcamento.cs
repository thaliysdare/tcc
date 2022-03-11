using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tcc.webapi.Enums;

namespace tcc.webapi.Models
{
    [Table("Orcamento")]
    public class Orcamento
    {
        public int OrcamentoId { get; set; }

        [Required]
        [ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey(nameof(Veiculo))]
        public int VeiculoId { get; set; }

        [Required]
        [ForeignKey(nameof(Cliente))]
        public int ClienteId { get; set; }

        [Required]
        public DateTime DataOrcamento { get; set; }

        [Required]
        public DateTime DataPrevisao { get; set; }

        [MaxLength(1000)]
        public string Observacao { get; set; }

        [Required]
        public double ValorOrcamento { get; set; }

        [ForeignKey(nameof(OrdemServico))]
        public int? OrdemServicoId { get; set; }

        [Required]
        public StatusOrcamentoEnum IdcStatusOrcamento { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual ICollection<ServicoOrcamento> ServicoOrcamento { get; set; }
    }
}
