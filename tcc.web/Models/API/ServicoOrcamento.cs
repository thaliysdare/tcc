namespace tcc.web.Models.API
{
    public class ServicoOrcamentoEnvio
    {
        public int? ServicoOrcamentoId { get; set; }
        public int? OrcamentoId { get; set; }
        public int ServicoId { get; set; }
        public double Valor { get; set; }
    }

    public class ServicoOrcamentoRetorno
    {
        public int ServicoOrcamentoId { get; set; }
        public int ServicoId { get; set; }
        public int OrcamentoId { get; set; }
        public double Valor { get; set; }
        public ServicoRetorno Servico { get; set; }
    }
}
