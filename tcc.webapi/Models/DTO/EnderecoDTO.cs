using System.ComponentModel.DataAnnotations;

namespace tcc.webapi.Models.DTO
{
    public class EnderecoEnvioDTO
    {
        [Required]
        public string Rua { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        public Endereco MapearModel()
        {
            return new Endereco()
            {
                Rua = this.Rua,
                Numero = this.Numero,
                Complemento = this.Complemento,
                CEP = this.CEP,
                Bairro = this.Bairro,
                Cidade = this.Cidade,
                Estado = this.Estado
            };
        }
    }

    public class EnderecoRetornoDTO
    {
        public int EnderecoId { get; set; }

        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string CEP { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public static EnderecoRetornoDTO MapearDTO(Endereco model)
        {
            return new EnderecoRetornoDTO()
            {
                EnderecoId = model.EnderecoId,
                Rua = model.Rua,
                Numero = model.Numero,
                Complemento = model.Complemento,
                CEP = model.CEP,
                Bairro = model.Bairro,
                Cidade = model.Cidade,
                Estado = model.Estado
            };
        }
    }
}
