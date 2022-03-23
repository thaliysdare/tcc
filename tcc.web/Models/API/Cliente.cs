using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tcc.web.Models.API
{
    public class ClienteEnvio
    {
        public int? ClienteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPFOuCNPJ { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public EnderecoEnvio Endereco { get; set; }
    }

    public class ClienteRetornoRoot
    {
        public List<ClienteRetorno> Clientes { get; set; }
    }

    public class ClienteRetorno
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        [JsonIgnore]
        public string NomeCompleto { get => $"{Nome} {Sobrenome}"; }
        public string Sobrenome { get; set; }
        public string CPFOuCNPJ { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public int? EnderecoId { get; set; }
        public bool Ativo { get; set; }
        public EnderecoRetorno Endereco { get; set; }
        public List<VeiculoRetorno> Veiculos { get; set; }

    }
}
