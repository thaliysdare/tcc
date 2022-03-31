namespace tcc.web.Models.API
{
    public class ServicoOrdemServicoEnvio
    {
        public int? ServicoOrdemServicoId { get; set; }
        public int? OrdemServicoId { get; set; }
        public int ServicoId { get; set; }
        public double Valor { get; set; }
    }

    public class ServicoOrdemServicoRetorno
    {
        public int ServicoOrdemServicoId { get; set; }
        public int ServicoId { get; set; }
        public int OrdemServicoId { get; set; }
        public double Valor { get; set; }
        public ServicoRetorno Servico { get; set; }
    }
}
