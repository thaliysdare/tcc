using System.ComponentModel.DataAnnotations;

namespace tcc.webapi.Models.DTO
{
    public class VeiculoEnvioDTO
    {
        [Required]
        public string Placa { get; set; }

        [Required]
        public string Marca { get; set; }

        public Veiculo MapearModel()
        {
            return new Veiculo()
            {
                Placa = this.Placa,
                Marca = this.Marca
            };
        }
    }

    public class VeiculoRetornoDTO
    {
        public int VeiculoId { get; set; }

        public int ClienteId { get; set; }

        public string Placa { get; set; }

        public string Marca { get; set; }

        public bool Ativo { get; set; }

        public static VeiculoRetornoDTO MapearDTO(Veiculo model)
        {
            return new VeiculoRetornoDTO()
            {
                VeiculoId = model.VeiculoId,
                ClienteId = model.ClienteId,
                Placa = model.Placa,
                Marca = model.Marca,
                Ativo = model.IdcStatusVeiculo == Enums.StatusVeiculoEnum.Ativo
            };
        }
    }
}
