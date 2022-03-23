using System.Collections.Generic;

namespace tcc.web.Models.API
{
    public class VeiculoEnvio
    {
        public int? VeiculoId { get; set; }
        public int ClienteId { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
    }

    public class VeiculoRetornoRoot
    {
        public List<VeiculoRetorno> ListaVeiculos { get; set; }
    }

    public class VeiculoRetorno
    {
        public int VeiculoId { get; set; }
        public int ClienteId { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public bool Ativo { get; set; }
    }
}
