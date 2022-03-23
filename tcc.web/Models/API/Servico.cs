using System.Collections.Generic;

namespace tcc.web.Models.API
{
    public class ServicoEnvio
    {
        public int? ServicoId { get; set; }
        public string Descricao { get; set; }
        public string DescricaoResumida { get; set; }
        public double Valor { get; set; }
    }

    public class ServicoRetornoRoot
    {
        public List<ServicoRetorno> ListaServicos { get; set; }
    }

    public class ServicoRetorno
    {
        public int ServicoId { get; set; }
        public string Descricao { get; set; }
        public string DescricaoResumida { get; set; }
        public double Valor { get; set; }
        public bool Ativo { get; set; }
    }
}
