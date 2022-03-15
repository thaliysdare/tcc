using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
                Endereco = this.Endereco.MapearModel()
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

        public int? EnderecoId{ get; set; }

        public EnderecoRetornoDTO Endereco{ get; set; }

        public static ClienteRetornoDTO MapearDTO(Cliente model)
        {
            return new ClienteRetornoDTO()
            {
                ClienteId = model.ClienteId,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                CPFOuCNPJ = model.CPFOuCNPJ,
                Telefone1 = model.Telefone1,
                Telefone2 = model.Telefone2,
                EnderecoId = model.EnderecoId,
                //Endereco = model.EnderecoId.HasValue ? EnderecoRetornoDTO.MapearDTO(model.Endereco) : default(EnderecoRetornoDTO) 
            };
        }
    }
}
