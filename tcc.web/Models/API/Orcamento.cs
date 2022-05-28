using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace tcc.web.Models.API
{
    public class OrcamentoEnvio
    {
        public int? OrcamentoId { get; set; }
        public int UsuarioId { get; set; }
        public int VeiculoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataPrevisao { get; set; }
        public string Observacao { get; set; }
        public int Situacao { get; set; }
        public List<ServicoOrcamentoEnvio> ListaItensServicos { get; set; }
    }

    public class OrcamentoRetornoRoot
    {
        public List<OrcamentoRetorno> Orcamentos { get; set; }
    }

    public class OrcamentoRetorno
    {
        public int OrcamentoId { get; set; }
        public int? OrdemServicoId { get; set; }
        public int UsuarioId { get; set; }
        public int VeiculoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataPrevisao { get; set; }
        public string Observacao { get; set; }
        public double ValorOrcamento { get; set; }
        public OrcamentoSituacaoEnum Situacao { get; set; }
        public List<ServicoOrcamentoRetorno> ListaItensServicos { get; set; }

        public ClienteRetorno Cliente { get; set; }
        public VeiculoRetorno Veiculo { get; set; }
        public UsuarioRetorno Usuario { get; set; }
    }

    public enum OrcamentoSituacaoEnum
    {
        [Description("Orçamento gerado")]
        OrcamentoGerado = 1,
        [Description("Ordem de serviço gerada")]
        OSGerada = 2
    }
}
