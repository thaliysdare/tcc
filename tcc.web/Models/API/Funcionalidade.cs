using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tcc.web.Models.API
{
    public class FuncionalidadeEnvio
    {
        public int? FuncionalidadeId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }

    public class FuncionalidadeRetornoRoot
    {
        public List<FuncionalidadeRetorno> Funcionalidades { get; set; }
    }

    public class FuncionalidadeRetorno
    {
        public int FuncionalidadeId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
