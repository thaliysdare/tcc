using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tcc.webapi.Enums;

namespace tcc.webapi.Models
{
    [Table("OrdemServico")]
    public class OrdemServico
    {
        public int OrdemServicoId { get; set; }

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
        public DateTime DataEntrada { get; set; }

        [Required]
        public DateTime DataPrevisao { get; set; }

        public DateTime? DataSaida { get; set; }

        [Required]
        public int KMAtual { get; set; }

        [MaxLength(1000)]
        public string Observacao { get; set; }

        [Required]
        public double ValorOrdemServico { get; set; }

        [Required]
        public StatusOrdemServicoEnum IdcStatusOrdemServico { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual ICollection<ServicoOrdemServico> ServicoOrdemServico { get; set; }

    }
}
