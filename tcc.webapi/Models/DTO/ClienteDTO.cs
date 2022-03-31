using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace tcc.webapi.Models.DTO
{
    public class ClienteEnvioDTO
    {
        [Required]
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CPFOuCNPJ { get; set; }

        [Required]
        public string Telefone1 { get; set; }

        public string Telefone2 { get; set; }

        public EnderecoEnvioDTO Endereco { get; set; }

        public Cliente MapearModel()
        {
            return new Cliente()
            {
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                CPFOuCNPJ = this.CPFOuCNPJ,
                Telefone1 = this.Telefone1,
                Telefone2 = this.Telefone2,
                Endereco = this.Endereco != null ? this.Endereco.MapearModel() : default(Endereco)
            };
        }
    }

    public class ClienteRetornoDTO
    {
        public int ClienteId { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CPFOuCNPJ { get; set; }

        public string Telefone1 { get; set; }

        public string Telefone2 { get; set; }

        public bool Ativo { get; set; }

        public int? EnderecoId { get; set; }

        public EnderecoRetornoDTO Endereco { get; set; }

        public List<VeiculoRetornoDTO> Veiculos { get; set; }

        public static ClienteRetornoDTO MapearDTO(Cliente model)
        {
            if (model == null) return null;

            var clienteDTO = new ClienteRetornoDTO()
            {
                ClienteId = model.ClienteId,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                CPFOuCNPJ = model.CPFOuCNPJ,
                Telefone1 = model.Telefone1,
                Telefone2 = model.Telefone2,
                EnderecoId = model.EnderecoId,
                Ativo = model.IdcStatusCliente == Enums.StatusClienteEnum.Ativo
            };

            if (model.Endereco != null) clienteDTO.Endereco = EnderecoRetornoDTO.MapearDTO(model.Endereco);

            if (model.Veiculo != null && model.Veiculo.Any())
                clienteDTO.Veiculos = model.Veiculo.Select(x => VeiculoRetornoDTO.MapearDTO(x)).ToList();

            return clienteDTO;
        }
    }
}
