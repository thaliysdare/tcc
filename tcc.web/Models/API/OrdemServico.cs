using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace tcc.web.Models.API
{
    public class OrdemServicoEnvio
    {
        public int? OrdemServicoId { get; set; }
        public int UsuarioId { get; set; }
        public int VeiculoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataPrevisao { get; set; }
        public DateTime? DataSaida { get; set; }
        public int KMAtual { get; set; }
        public string Observacao { get; set; }
        public int Situacao { get; set; }
        public List<ServicoOrdemServicoEnvio> ListaItensServicos { get; set; }
    }

    public class OrdemServicoRetornoRoot
    {
        public List<OrdemServicoRetorno> OrdemServicos { get; set; }
    }

    public class OrdemServicoRetorno
    {
        public int OrdemServicoId { get; set; }
        public int UsuarioId { get; set; }
        public int VeiculoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataPrevisao { get; set; }
        public DateTime? DataSaida { get; set; }
        public int KMAtual { get; set; }
        public string Observacao { get; set; }
        public double ValorOrdemServico { get; set; }
        public OrdemServicoSituacaoEnum Situacao { get; set; }
        public List<ServicoOrdemServicoRetorno> ListaItensServicos { get; set; }

        public ClienteRetorno Cliente { get; set; }
        public VeiculoRetorno Veiculo { get; set; }
        public UsuarioRetorno Usuario { get; set; }
    }

    public enum OrdemServicoSituacaoEnum
    {
        [Description("Ordem de serviço gerada")]
        OSGerada = 1,
        [Description("Ordem de serviço em andamento")]
        OSEmAndamento = 2,
        [Description("Ordem de serviço paralizada")]
        OSParalizada = 3,
        [Description("Ordem de serviço finalizada")]
        OSFinalizada = 4,
        [Description("Ordem de serviço cancelada")]
        OSCancelada = 5
    }
}
